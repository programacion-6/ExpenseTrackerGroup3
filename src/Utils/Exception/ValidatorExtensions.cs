using ExpenseTrackerGroup3.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace ExpenseTrackerGroup3.Utils.Exception;

public static class ValidatorExtensions
{
    public static T ThrowIfNull<T>(this T? obj, string message) where T : class
    {
        if (obj == null)
        {
            throw new NotFoundException(message);
        }
        return obj;
    }

    public static void ThrowIfOperationFailed(this bool success, string message)
    {
        if (!success)
        {
            throw new InternalServerErrorException(message);
        }
    }

    public static void ThrowIfEmpty<T>(this IEnumerable<T> collection, string message)
    {
        if (!collection.Any())
        {
            throw new NotFoundException(message);
        }
    }

    public static void ThrowIfExists<T>(this T? obj, string message) where T : class
    {
        if (obj != null)
        {
            throw new BadRequestException(message);
        }
    }

    public static void ThrowIfExists<T>(this IEnumerable<T> collection, string message)
    {
        if (collection.Any())
        {
            throw new BadRequestException(message);
        }
    }

    public static void ThrowIfValidationFailed(this ValidationResult validateResult)
    {
        if (!validateResult.IsValid)
        {
            throw new ValidationException(validateResult.Errors);
        }
    }
}
