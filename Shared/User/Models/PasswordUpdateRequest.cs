namespace StudyMate.Shared.User.Models;

public class PasswordUpdateRequest
{
    public string Username { get; set; } = null!;
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
