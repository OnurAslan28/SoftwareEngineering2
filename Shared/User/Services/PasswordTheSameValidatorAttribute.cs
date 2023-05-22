namespace StudyMate.Shared.User.Services;

using System.ComponentModel.DataAnnotations;
using Models;

public class PasswordTheSameValidatorAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		return ((PasswordChangeRequest)validationContext.ObjectInstance).NewPassword != value!.ToString()
			? new ValidationResult("The password and confirmation password do not match.")
			: ValidationResult.Success;
	}
}