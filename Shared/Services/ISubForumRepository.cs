namespace StudyMate.Shared.Services;

using Models;

public interface ISubForumRepository
{
	Task<List<Post>> GetLatestPosts(int numOfPosts);
}