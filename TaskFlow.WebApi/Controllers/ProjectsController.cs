using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            try
            {
                var projects = await _projectService.GetAllAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Error interno del servidor
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(id);

                if (project == null)
                {
                    // No se encontró el proyecto
                    return NotFound("Project not found.");
                }

                return Ok(project);
            }
            catch (Exception ex)
            {
                // Error interno del servidor
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            try
            {
                var created = await _projectService.CreateAsync(project);
                // Return 201 Created + Location header pointing to GetProject
                return CreatedAtAction(
                    nameof(GetProject),          // Action to route to (GetProject method)
                    new { id = created.Id },     // Route values
                    created                      // Response body
                );
            }
            catch (Exception ex)
            {
                // Error interno al crear el proyecto
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/projects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                // El ID no coincide
                return BadRequest("Project ID does not match.");
            }

            try
            {
                var success = await _projectService.UpdateAsync(project);

                if (!success)
                {
                    // Proyecto no encontrado
                    return NotFound("Project not found.");
                }

                return Ok(project);
            }
            catch (Exception ex)
            {
                // Error interno al actualizar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var success = await _projectService.DeleteAsync(id);

                if (!success)
                {
                    // No se encontró el proyecto
                    return NotFound("Project not found.");
                }

                return Ok("Project deleted successfully.");
            }
            catch (Exception ex)
            {
                // Error interno al eliminar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
