using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            // Domain validation
            if (string.IsNullOrWhiteSpace(project.Name))
                throw new ArgumentException("Project name is required.");
            // (El nombre del proyecto es obligatorio)

            if (string.IsNullOrWhiteSpace(project.Description))
                throw new ArgumentException("Project description is required.");
            // (La descripción es obligatoria)

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            var existing = await _context.Projects.FindAsync(project.Id);
            if (existing == null)
                return false;

            // Domain validation
            if (string.IsNullOrWhiteSpace(project.Name))
                throw new ArgumentException("Project name is required.");

            if (string.IsNullOrWhiteSpace(project.Description))
                throw new ArgumentException("Project description is required.");

            // Apply changes cleanly
            _context.Entry(existing).CurrentValues.SetValues(project);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
