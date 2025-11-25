using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validators
{
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required."); // Nombre requerido

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description can have up to 500 characters."); // Límite opcional
        }
    }
}
