using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // "Error interno" 
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);

                if (task == null)
                {
                    // "La tarea no existe."
                    return NotFound("Task not found.");
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
        {
            try
            {
                var created = await _taskService.CreateAsync(taskItem);

                return CreatedAtAction(
                    nameof(GetTask),
                    new { id = created.Id },
                    created
                );
            }
            catch (ArgumentException ex)
            {
                // "El proyecto asociado no existe."
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                // "El ID de la tarea no coincide"
                return BadRequest("Task ID does not match.");
            }

            try
            {
                var updated = await _taskService.UpdateAsync(taskItem);

                if (!updated)
                {
                    // "La tarea no existe."
                    return NotFound("Task not found.");
                }

                return Ok("Task updated successfully.");
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var deleted = await _taskService.DeleteAsync(id);

                if (!deleted)
                {
                    // "La tarea no existe."
                    return NotFound("Task not found.");
                }

                return Ok("Task deleted successfully.");
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
