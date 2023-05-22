namespace StudyMate.Shared.Services;

using Models;

public interface IGroupRepository
{
	Task<Group?> FindAsync(int groupId);
	Task<int> CreateGroup(Group group);
	Task<List<Group>> GetOpenGroups(int numOfGroups);
}