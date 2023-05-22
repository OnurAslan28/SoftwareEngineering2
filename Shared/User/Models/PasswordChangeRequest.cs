namespace StudyMate.Shared.User.Models;

using System.ComponentModel.DataAnnotations;
using Services;

public class PasswordChangeRequest
{
	[Required]
	public string CurrentPassword { get; set; } = null!;

	[Required]
	[PasswordValidator]
	public string NewPassword { get; set; } = null!;

	[Required]
	[PasswordTheSameValidator]
	public string ConfirmNewPassword { get; set; } = null!;
}