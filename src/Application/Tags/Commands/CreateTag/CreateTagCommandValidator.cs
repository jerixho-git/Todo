using FluentValidation;

namespace Todo_App.Application.Tags.Commands.CreateTag;
public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Tag name is required.")
            .MaximumLength(50).WithMessage("Tag name must be at most 50 characters.")
            .Must(name => !string.IsNullOrWhiteSpace(name?.Trim()))
            .WithMessage("Tag name cannot be whitespace.");
    }
}