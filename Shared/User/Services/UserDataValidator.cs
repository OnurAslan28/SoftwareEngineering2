namespace StudyMate.Shared.User.Services;

public static class UserDataValidator
{
	public const string TokenKey = "StudyMateLoginToken";
	
	private const int UsernameMinLength = 1;
	private const int UsernameMaxLength = 20;

	private const int PasswordMinLength = 3;

	public static bool IsUsernameValid(string? username, out string message)
	{
		if (IsEmpty(username))
		{
			message = "Username can't be empty";
			return false;
		}

		username = username!.Trim();

		if (!UsernameHasRightLength(username))
		{
			message = $"Username has to be between {UsernameMinLength} and {UsernameMaxLength} characters long";
			return false;
		}

		message = $"{username} is a valid username";
		return true;
	}

	public static bool IsPasswordValid(in string? password, out string message)
	{
		if (!PasswordHasRightLength(password))
		{
			message = $"Password has to be at least {PasswordMinLength} characters long";
			return false;
		}

		message = "Password is valid";
		return true;
	}

	#region [ Password guards ]

	// Password has to be at least 3 characters
	private static bool PasswordHasRightLength(in string? password) => password?.Length >= PasswordMinLength;

	#endregion

	#region [ Username guards ]

	// Username can't be empty
	private static bool IsEmpty(in string? username) => string.IsNullOrEmpty(username);

	// Username has to be between 3 and 30 characters long
	private static bool UsernameHasRightLength(in string username) => username.Length is >= UsernameMinLength and <= UsernameMaxLength;

	#endregion
}