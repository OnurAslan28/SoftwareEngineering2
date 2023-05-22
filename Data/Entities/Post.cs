# nullable disable
namespace StudyMate.Data.Entities;

public class Post
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool Archived { get; set; }
	public string Content { get; set; }
	public DateTime Posted { get; set; }
	public SubForum SubForum { get; set; }
	public User Author { get; set; }
	public ICollection<Tag> Tags { get; set; }
}