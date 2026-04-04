using FluentValidation;

namespace Application.Tags.Commands.Delete;

public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Tag ID must be greater than 0");
    }
}
