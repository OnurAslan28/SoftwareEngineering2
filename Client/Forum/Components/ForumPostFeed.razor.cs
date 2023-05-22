namespace StudyMate.Client.Forum.Components;

using Shared.Models;

public partial class ForumPostFeed
{
	List<Post>? Posts { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Posts = await Api.GetPosts();
		await base.OnInitializedAsync();
	}
}