using Application.Tags.Delete;
using FluentValidation;

namespace Application.Tags.Delete;

public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Tag ID must be greater than 0");
    }
}