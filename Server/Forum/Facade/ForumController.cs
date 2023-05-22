namespace StudyMate.Server.Forum.Facade;

using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Services;

[Route("api/[controller]")]
[ApiController]
public class ForumController : ControllerBase
{
	private readonly ISubForumRepository _subForumRepo;

	public ForumController(ISubForumRepository subForumRepo)
	{
		_subForumRepo = subForumRepo;
	}

	[HttpGet("schwarzes-brett")]
	public async Task<List<Post>> GetPosts([FromQuery] int numOfPosts = 20)
	{
		return await _subForumRepo.GetLatestPosts(numOfPosts);
	}
}
