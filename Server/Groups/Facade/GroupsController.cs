namespace StudyMate.Server.Groups.Facade;

using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Services;
using User.Services;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
	private readonly IGroupRepository _groupRepo;

	public GroupsController(IGroupRepository groupRepo)
	{
		_groupRepo = groupRepo;
	}

	[HttpGet]
	public async Task<List<Group>> GetOpenGroups([FromQuery] int numOfGroups = 20)
	{
		return await _groupRepo.GetOpenGroups(numOfGroups);
	}

	[HttpPost]
	[LoggedIn]
	public async Task<int> CreateGroup([FromBody] Group group)
	{
		// evtl noch checks
		return await _groupRepo.CreateGroup(group);
	}
}