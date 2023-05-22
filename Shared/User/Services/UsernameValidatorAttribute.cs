namespace StudyMate.Shared.User.Services;

using System.ComponentModel.DataAnnotations;

public class UsernameValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return UserDataValidator.IsUsernameValid(value as string, out var err)
            ? ValidationResult.Success
            : new ValidationResult(err, new[] { validationContext.DisplayName });
    }
}