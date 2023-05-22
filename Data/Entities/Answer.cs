# nullable disable
namespace StudyMate.Data.Entities;

public class Answer
{
	public int Id { get; set; }
	public int Rating { get; set; }
	public string Content { get; set; }
	public DateTime Posted { get; set; }
	public User Author { get; set; }
	public Post Post { get; set; }
}
