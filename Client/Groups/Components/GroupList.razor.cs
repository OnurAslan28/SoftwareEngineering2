namespace StudyMate.Client.Groups.Components;

using Shared.Models;

public partial class GroupList
{
	List<Group>? Groups { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Groups = await Api.GetOpenGroups();
		await base.OnInitializedAsync();
	}
}