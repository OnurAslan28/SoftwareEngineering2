namespace StudyMate.Shared.Models;

public record Post(
	string Title,
	bool Archived,
	string Content,
	DateTime Posted,
	string Sub,
	User Author,
	ICollection<string> Tags);