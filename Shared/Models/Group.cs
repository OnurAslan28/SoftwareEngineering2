namespace StudyMate.Shared.Models;

public record Group(
	string OwnerUsername,
	string Name,
	uint Capacity,
	ICollection<string> Tags,
	ICollection<User> Members)
{
	public bool Open => Capacity - Members.Count > 0;
}