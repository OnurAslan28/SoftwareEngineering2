#nullable disable
namespace StudyMate.Data.Entities;

public class Group
{
	public int Id { get; set; }
	public string OwnerUsername { get; set; }
	public string Name { get; set; }
	public uint Capacity { get; set; }
	public ICollection<Tag> Tags { get; set; }
	public ICollection<User> Members { get; set; }
}