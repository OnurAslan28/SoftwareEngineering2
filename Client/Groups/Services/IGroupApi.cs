namespace StudyMate.Client.Groups.Services;

using RestEase;
using Shared.Models;

[BasePath("api")]
public interface IGroupApi
{
    [Get("groups")]
    public Task<List<Group>> GetOpenGroups([Query] int numOfGroups = 20);
}