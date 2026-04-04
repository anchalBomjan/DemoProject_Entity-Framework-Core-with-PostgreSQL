using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class AppValidationException : Exception
{
    public List<string> Errors { get; }

    public AppValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation errors occurred.")
    {
        Errors = failures.Select(f => f.ErrorMessage).ToList();
    }
}
