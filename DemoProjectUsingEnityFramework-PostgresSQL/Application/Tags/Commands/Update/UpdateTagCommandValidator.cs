using FluentValidation;

namespace Application.Tags.Commands.Update;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Tag ID must be greater than 0");

        RuleFor(x => x.Tag.Name)
            .NotEmpty().WithMessage("Tag name is required")
            .MaximumLength(100).WithMessage("Tag name must not exceed 100 characters");
    }
}
