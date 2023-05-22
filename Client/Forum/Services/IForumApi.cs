namespace StudyMate.Client.Forum.Services;

using RestEase;
using Shared.Models;

[BasePath("api")]
public interface IForumApi
{
    [Get("forum/schwarzes-brett")]
    public Task<List<Post>> GetPosts([Query]int numOfPosts = 20);
}
