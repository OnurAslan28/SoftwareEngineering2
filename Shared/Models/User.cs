namespace StudyMate.Shared.Models;

public record User(string Username, byte[] ProfilePicture, string? BioInfo = null, string? CourseOfStudy = null)
{
	public byte[] ProfilePicture { get; set; } = ProfilePicture;
	public string? BioInfo { get; set; } = BioInfo;
	public string? CourseOfStudy { get; set; } = CourseOfStudy;
}
