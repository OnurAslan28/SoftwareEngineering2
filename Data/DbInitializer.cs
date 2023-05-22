namespace StudyMate.Data;

using System;
using Entities;

public static class DbInitializer
{
	public static void Initialize(StudyMateContext context)
	{
		if (context.Users.Any())
		{
			return;   // DB has been seeded
		}

		#region creating some users
		var users = new User[]
		{
				new User{Username="Carson", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Meredith", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Arturo", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Gytis", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Yan", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Peggy", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Laura", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
				new User{Username="Nino", Password=new byte[5], ProfilePicture=new ProfilePicture{ImageData=new byte[5]}},
		};

		context.Users.AddRange(users);
		context.SaveChanges();

		// for later references
		var userCarson = context.Users
			.Single(u => u.Username == "Carson");
		var userMeredith = context.Users
			.Single(u => u.Username == "Meredith");
		var userArturo = context.Users
			.Single(u => u.Username == "Arturo");
		var members1 = new List<User>() { userCarson };
		var members2 = new List<User>() { userCarson, userMeredith };
		var members3 = new List<User>() { userCarson, userMeredith, userArturo };
		#endregion

		#region creating some tags
		var tags = new Tag[]
		{
				new Tag{Name="SE2"},
				new Tag{Name="IS"},
				new Tag{Name="RN"},
				new Tag{Name="Lerngruppe"},
				new Tag{Name="Allgemein"}
		};
		context.Tags.AddRange(tags);
		context.SaveChanges();

		// for later references
		var tagLerngruppe = context.Tags
			.Single(t => t.Name == "Lerngruppe");
		var tagSE2 = context.Tags
			.Single(t => t.Name == "SE2");
		var tagIS = context.Tags
			.Single(t => t.Name == "IS");
		var tagAllg = context.Tags
			.Single(t => t.Name == "Allgemein");
		var tagList1 = new List<Tag> { tagLerngruppe };
		var tagList2 = new List<Tag> { tagSE2 };
		var tagList3 = new List<Tag> { tagIS, tagLerngruppe };
		var tagList4 = new List<Tag> { tagAllg };
		#endregion

		#region creating some groups

		var groups = new Group[]
		{
				new Group{Name="Gruppe 1 💕", Capacity=10, Tags=tagList1, Members=members1, OwnerUsername="Carson"},
				new Group{Name="Team 2", Capacity=5 , Tags=tagList2, Members=members2, OwnerUsername="Carson"},
				new Group{Name="Gruppe D", Capacity=6 , Tags=tagList3, Members=members3 ,OwnerUsername="Carson"}
		};

		context.Groups.AddRange(groups);
		context.SaveChanges();

		userCarson.Groups.Add(groups
			.Where(g => g.Name == "Gruppe 1 💕")
			.FirstOrDefault()!);
		userCarson.Groups.Add(groups!
			.Where(g => g.Name == "Team 2")
			.FirstOrDefault()!);
		userMeredith.Groups.Add(groups
			.Where(g => g.Name == "Team 2")
			.FirstOrDefault()!);
		userCarson.Groups.Add(groups
			.Where(g => g.Name == "Gruppe D")
			.FirstOrDefault()!);
		userMeredith.Groups.Add(groups
			.Where(g => g.Name == "Gruppe D")
			.FirstOrDefault()!);
		userArturo.Groups.Add(groups
			.Where(g => g.Name == "Gruppe D")
			.FirstOrDefault()!);

		context.SaveChanges();
		#endregion

		#region creating some subs
		var subs = new SubForum[]
		{
				new SubForum{Title="Studienalltag"}
		};

		context.SubForums.AddRange(subs);
		context.SaveChanges();
		#endregion

		#region creating some posts
		var posts = new Post[]
		{
				new Post
				{
					Title="Was gibt es heute in der Mensa?",
					Author=userMeredith,
					Content="Weiß jemand von euch was es heute in der Mensa zu essen gibt? Kann man das irgendwo nachsehen?",
					Posted=DateTime.Today,
					SubForum=subs.First(),
					Tags=tagList4
				}
		};
		context.Posts.AddRange(posts);
		context.SaveChanges();

		var mensaPost = posts.FirstOrDefault(p => p.Id == 1);
		#endregion

		#region creating some answers
		var answers = new Answer[]
		{
					new Answer
					{
						Author=userArturo,
						Post=mensaPost!,
						Content="Ich glaube schon wieder Currywurst...",
						Posted=DateTime.Today
					}
		};
		context.Answers.AddRange(answers);
		context.SaveChanges();
		#endregion
	}
}