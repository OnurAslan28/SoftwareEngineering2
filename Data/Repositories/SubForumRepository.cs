namespace StudyMate.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using StudyMate.Shared.Services;

public class SubForumRepository : ISubForumRepository
{
	private readonly StudyMateContext _context;

	public SubForumRepository(StudyMateContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<List<Shared.Models.Post>> GetLatestPosts(int numOfPosts)
	{
		return await _context.Posts
		 	.Include(p => p.Author).ThenInclude(a => a.ProfilePicture)
			.Include(p => p.Tags)
			.Include(p => p.SubForum)
			.OrderByDescending(p => p.Posted)
			.Take(numOfPosts)
			.Select(p => new Shared.Models.Post(p.Title, false, p.Content, p.Posted, p.SubForum.Title, p.Author.ToModel(), p.Tags.Select(t => t.Name).ToList()))
			.AsNoTracking()
			.ToListAsync();
	}
}