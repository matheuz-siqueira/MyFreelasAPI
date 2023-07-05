using FluentValidation.Results;

namespace myfreelas.Exceptions.ErrorsValidators;

public static class ValidationFailuresExtension
{
    public static IList<UserValidationFailure> ToUserValidationFailure(
        this IList<ValidationFailure> failures)
    {
        return failures
            .Select(f => new UserValidationFailure(f.PropertyName, f.ErrorMessage))
                .ToList();
    }
}
