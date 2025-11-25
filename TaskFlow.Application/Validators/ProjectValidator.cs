using FluentValidation;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Validators
{
 public class ProjectValidator : AbstractValidator<Project>
 {
 public ProjectValidator()
 {
 RuleFor(x => x.Name)
 .NotEmpty().WithMessage("Project name is required.")
 .MinimumLength(1).WithMessage("Project name must not be empty.");

 RuleFor(x => x.Description)
 .MaximumLength(500).WithMessage("Description can have up to500 characters.");

 RuleFor(x => x.UserId)
 .GreaterThan(0).WithMessage("Project must be associated with a valid user.");
 }
 }
}