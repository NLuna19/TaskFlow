using FluentValidation;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Validators
{
 public class TaskItemValidator : AbstractValidator<TaskItem>
 {
 public TaskItemValidator()
 {
 RuleFor(x => x.Title)
 .NotEmpty().WithMessage("Task title is required.")
 .MinimumLength(1).WithMessage("Task title must not be empty.");

 RuleFor(x => x.DueDate)
 .GreaterThanOrEqualTo(x => x.CreatedAt).When(x => x.DueDate.HasValue)
 .WithMessage("DueDate must be after CreatedAt.");

 RuleFor(x => x.ProjectId)
 .GreaterThan(0).WithMessage("Task must belong to a valid project.");
 }
 }
}