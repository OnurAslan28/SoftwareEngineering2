namespace StudyMate.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public string Username { get; set; } = null!;
	public byte[] Password { get; set; } = null!;
	public string? BioInfo { get; set; }
	public string? CourseOfStudy { get; set; }
	public ICollection<Group>? Groups { get; set; }
	public ProfilePicture ProfilePicture { get; set; } = null!;

	public Shared.Models.User ToModel() => new(Username, ProfilePicture.ImageData, BioInfo, CourseOfStudy);
}