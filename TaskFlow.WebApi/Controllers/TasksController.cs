using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            try
            {
                var task = await _context.Tasks
                    .Include(t => t.Project)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (task == null)
                    return NotFound("La tarea no existe.");

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
        {
            try
            {
                // Validamos que el Project exista
                var projectExists = await _context.Projects.AnyAsync(p => p.Id == taskItem.ProjectId);
                if (!projectExists)
                    return BadRequest("El proyecto asociado no existe.");

                _context.Tasks.Add(taskItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
                return BadRequest("El ID de la tarea no coincide.");

            try
            {
                // Validamos existencia
                if (!await _context.Tasks.AnyAsync(t => t.Id == id))
                    return NotFound("La tarea no existe.");

                _context.Entry(taskItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Tarea actualizada correctamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error de concurrencia al actualizar la tarea.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                    return NotFound("La tarea no existe.");

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return Ok("Tarea eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
