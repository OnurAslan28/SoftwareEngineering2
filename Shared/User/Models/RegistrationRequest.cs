namespace StudyMate.Shared.User.Models;

using Services;

public class RegistrationRequest
{
    [UsernameValidator]
    public string Username { get; set; } = null!;
    [PasswordValidator]
    public string Password { get; set; } = null!;

    public bool AutoLogin { get; set; } = true;
}