namespace StudyMate.Shared.Models;

public record Answer(
	int Rating,
	string Content,
	DateTime Posted,
	User Author,
	Post Post
	);