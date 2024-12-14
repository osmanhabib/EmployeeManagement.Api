using FluentValidation.Results;
using System.Data;

namespace EmployeeManagement.Extensions;

public static class ValidationResultExtensions
{
    public static bool EnsureValidResult(this ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            throw new ValidationResultException(string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage)));
        }
        else
        {
            return true;
        }
    }
}

public class ValidationResultException(string? s) : DataException(s)
{
}
