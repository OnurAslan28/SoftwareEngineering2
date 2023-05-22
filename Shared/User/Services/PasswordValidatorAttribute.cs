namespace StudyMate.Shared.User.Services;

using System.ComponentModel.DataAnnotations;

public class PasswordValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return UserDataValidator.IsPasswordValid(value as string, out var err)
            ? ValidationResult.Success
            : new ValidationResult(err, new[] { validationContext.DisplayName });
    }
}