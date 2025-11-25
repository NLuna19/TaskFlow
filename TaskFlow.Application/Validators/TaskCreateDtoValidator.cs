using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validators
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required."); // Título requerido

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be greater than 0."); // Debe existir proyecto

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0."); // Debe existir usuario
        }
    }
}
