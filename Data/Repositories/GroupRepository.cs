namespace StudyMate.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using Entities;
using StudyMate.Shared.Services;

public class GroupRepository : IGroupRepository
{
	private readonly StudyMateContext _context;

	public GroupRepository(StudyMateContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<Shared.Models.Group?> FindAsync(int groupId)
	{
		var groupEntity = await _context.Groups
			.Include(g => g.Members).ThenInclude(m => m.ProfilePicture)
			.Include(g => g.Tags)
			.SingleOrDefaultAsync(g => g.Id == groupId);

		if (groupEntity is null)
		{
			return null;
		}
		else
		{
			return new Shared.Models.Group(
				 groupEntity.OwnerUsername,
				 groupEntity.Name,
				 groupEntity.Capacity,
				 groupEntity.Tags
					.Select(t => t.Name).ToList(),
				 groupEntity.Members
					.Select(m => m.ToModel()).ToList()
			);
		}
	}

	public async Task<int> CreateGroup(Shared.Models.Group group)
	{
		var groupEntity = new Group
		{
			OwnerUsername = group.OwnerUsername,
			Name = group.Name,
			Capacity = group.Capacity,
			Members = group.Members.Select(m => new User()
			{
				Username = m.Username
			}).ToList(),
		};
		_context.Groups.Add(groupEntity);
		await _context.SaveChangesAsync();
		return groupEntity.Id;
	}

	public async Task<List<Shared.Models.Group>> GetOpenGroups(int numOfGroups)
	{
		var groups = await _context.Groups
			.Include(g => g.Tags)
			.Include(g => g.Members).ThenInclude(m => m.ProfilePicture)
			.Where(g => g.Capacity > g.Members.Count) // nur offene Gruppen
			.Take(numOfGroups)
			.Select(g => new Shared.Models.Group(
				 g.OwnerUsername,
				 g.Name,
				 g.Capacity,
				 g.Tags
					.Select(t => t.Name).ToList(),
				 g.Members
					.Select(m => new Shared.Models.User(m.Username, m.ProfilePicture.ImageData, null, null)).ToList()
			))
			.AsNoTracking()
			.ToListAsync();
		return groups;
	}
}