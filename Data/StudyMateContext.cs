#nullable disable
namespace StudyMate.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Entities;

public class StudyMateContext : DbContext
{

	public StudyMateContext(DbContextOptions<StudyMateContext> options)
		: base(options)
	{
	}

	public DbSet<LoginToken> LoginTokens { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<SubForum> SubForums { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<Answer> Answers { get; set; }
	public DbSet<ProfilePicture> ProfilePictures { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<LoginToken>().ToTable("LoginToken");
		modelBuilder.Entity<Tag>().ToTable("Tag");
		modelBuilder.Entity<User>().ToTable("User");
		modelBuilder.Entity<Group>().ToTable("Group");
		modelBuilder.Entity<SubForum>().ToTable("SubForum");
		modelBuilder.Entity<Post>().ToTable("Post");
		modelBuilder.Entity<Answer>().ToTable("Answer");
		modelBuilder.Entity<ProfilePicture>().ToTable("ProfilePicture");
	}
}