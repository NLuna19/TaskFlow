using FluentValidation;
using TaskFlow.Domain.Entities;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Application.Interfaces.Repositories;

namespace TaskFlow.Application.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IValidator<TaskItem> _validator;

    public TaskService(ITaskRepository repository, IValidator<TaskItem> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        var validation = await _validator.ValidateAsync(task);

        if (!validation.IsValid)
            throw new ValidationException(
                "Invalid task data", // Datos de la tarea inválidos
                validation.Errors
            );

        return await _repository.CreateAsync(task);
    }

    public async Task<bool> UpdateAsync(TaskItem task)
    {
        var validation = await _validator.ValidateAsync(task);

        if (!validation.IsValid)
            throw new ValidationException(
                "Invalid task data", // Datos de la tarea inválidos
                validation.Errors
            );

        return await _repository.UpdateAsync(task);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
