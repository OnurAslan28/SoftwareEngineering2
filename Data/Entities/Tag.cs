# nullable disable
namespace StudyMate.Data.Entities;

public class Tag
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<Group> Groups { get; set; }
	public ICollection<Post> Posts { get; set; }
}