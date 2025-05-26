using System.ComponentModel.DataAnnotations;

namespace PaSho_Tracker.Application.Services;

public static class ValidationService
{
    public static List<ValidationResult> Validate<T>(T entity)
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(entity, context, results, validateAllProperties: true);
        return results;
    }

    public static bool IsValid<T>(T entity)
    {
        return !Validate(entity).Any();
    }
}