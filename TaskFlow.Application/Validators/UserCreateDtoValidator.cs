using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.") // Username es requerido
                .MinimumLength(3).WithMessage("Username must have at least 3 characters."); // Al menos 3 caracteres

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.") // Email requerido
                .EmailAddress().WithMessage("Invalid email format."); // Formato válido

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.") // Password necesaria
                .MinimumLength(6).WithMessage("Password must have at least 6 characters."); // Mínimo 6 caracteres
        }
    }
}
