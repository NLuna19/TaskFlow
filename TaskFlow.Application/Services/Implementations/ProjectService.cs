using FluentValidation;
using TaskFlow.Domain.Entities;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Application.Interfaces.Repositories;

namespace TaskFlow.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;
    private readonly IValidator<Project> _validator;

    public ProjectService(IProjectRepository repository, IValidator<Project> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        var validation = await _validator.ValidateAsync(project);

        if (!validation.IsValid)
            throw new ValidationException(
                "Invalid project data", // Datos del proyecto inválidos
                validation.Errors
            );

        return await _repository.CreateAsync(project);
    }

    public async Task<bool> UpdateAsync(Project project)
    {
        var validation = await _validator.ValidateAsync(project);

        if (!validation.IsValid)
            throw new ValidationException(
                "Invalid project data", // Datos del proyecto inválidos
                validation.Errors
            );

        return await _repository.UpdateAsync(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
