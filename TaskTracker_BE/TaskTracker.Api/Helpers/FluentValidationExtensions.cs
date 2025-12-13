
using FluentValidation;
using TaskTracker.API.Helpers.Exceptions;

namespace TaskTracker.API.Helpers;

public static class FluentValidationExtensions
{
    public static void ValidateAndThrowValidationException<T>(this IValidator<T> validator, T instance)
    {
        var res = validator.Validate(instance);

        if (!res.IsValid)
        {
            var errors = res.Errors.Select(error => error.ErrorMessage);
            throw new ConflictException("A validation error occured.", errors);
        }
    }
}