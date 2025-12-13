using FluentValidation;
using TaskTracker.Api.Data.DTO;

namespace TaskTracker.API.Data.Validators;

public class TaskValidator : AbstractValidator<TicketDTO>
{
    public TaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Task title must be at least 3 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Task description must be at least 5 characters");

        RuleFor(x => x.Priority)
            .NotNull()
            .WithMessage("Task priority is required");
    }
}