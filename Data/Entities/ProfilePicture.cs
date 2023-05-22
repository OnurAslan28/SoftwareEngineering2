#nullable disable
namespace StudyMate.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class ProfilePicture
{
	[Key]
	public int Id { get; set; }
	public byte[] ImageData { get; set; }
	public bool ForDefault { get; set; }
}