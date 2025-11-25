using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskFlow.Application.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            // Basic domain validation
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Title is required.");
            // (Título es obligatorio)

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            var existing = await _context.Tasks.FindAsync(task.Id);
            if (existing == null)
                return false;

            // Basic domain validation
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Title is required.");
            // (Título es obligatorio)

            // Update values
            _context.Entry(existing).CurrentValues.SetValues(task);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
