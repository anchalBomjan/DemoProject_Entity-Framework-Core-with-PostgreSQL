using FluentValidation;

namespace Application.Tags.Commands.Create;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Tag.Name)
            .NotEmpty().WithMessage("Tag name is required")
            .MaximumLength(100).WithMessage("Tag name must not exceed 100 characters");
    }
}
