using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            try
            {
                var projects = await _context.Projects.ToListAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal service error: {ex.Message}");
            }
        }

        // GET: api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                var project = await _context.Projects
                 .Include(p => p.Tasks)
                 .FirstOrDefaultAsync(p => p.Id == id);

                if (project == null)
                    return NotFound();
                 return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // POST: api/projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/projects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
                return BadRequest("El ID del proyecto no coincide.");

            try
            {
                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(project);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Projects.AnyAsync(p => p.Id == id))
                    return NotFound("No se encontró el proyecto especificado.");
                else
                    return StatusCode(500, "Error al actualizar el proyecto (concurrencia).");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                    return NotFound("No se encontró el proyecto especificado.");

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return Ok("Proyecto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
