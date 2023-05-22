namespace StudyMate.Client.Test.Groups.Components;

using System.Collections.Generic;
using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudyMate.Client.Groups.Components;
using StudyMate.Client.Groups.Services;
using Shared.Models;
using TestContext = Bunit.TestContext;

[TestClass]
public class GroupListTest
{
	private TestContext context = null!;
	private Mock<IGroupApi> groupApiMock = null!;

	[TestInitialize]
	public void Setup()
	{
		context = new();
		groupApiMock = new Mock<IGroupApi>(MockBehavior.Strict);
		context.Services.AddSingleton(_ => groupApiMock.Object);
	}

	private static IEnumerable<Group> GenerateGroups(int numberOfGroups)
	{
		IEnumerable<User> generateMembers(uint numberOfMembers)
		{
			for (uint member = 0; member < numberOfMembers; member++)
			{
				yield return new User($"Member{member}", new byte[5]);
			}
		}

		IEnumerable<string> generateTags(uint numberOfTags)
		{
			for (uint tag = 0; tag < numberOfTags; tag++)
			{
				yield return $"Tag{tag}";
			}
		}

		for (uint i = 1; i <= numberOfGroups; i++)
		{
			var owner = new User($"Owner{i}", new byte[5]);

			yield return new Group(
				 owner.Username,
				 $"Group{i}",
				 i * 2,
				 generateTags(i).ToList(),
				 generateMembers(i).Append(owner).ToList()
				 );
		}
	}

	[TestMethod]
	public void WithGroups_ShouldDisplayGroups()
	{
		// Given 5 groups
		const int numOfGroups = 5;
		var groups = GenerateGroups(numOfGroups).ToList();
		groupApiMock.Setup(api => api.GetOpenGroups(20)).ReturnsAsync(groups);

		// When the group list is rendered
		var groupList = context.RenderComponent<GroupList>();

		// It should display 5 groups
		Assert.AreEqual(numOfGroups, groupList.FindAll(".group").Count, "Wrong number of groups rendered");
	}

	[TestMethod]
	public void WithoutGroups_ShouldDisplayNoGroups()
	{
		// Given no groups
		groupApiMock.Setup(api => api.GetOpenGroups(20)).ReturnsAsync(new List<Group>(0));

		// When the group list is rendered
		var groupList = context.RenderComponent<GroupList>();

		// It should display no groups
		Assert.AreEqual(0, groupList.FindAll(".group").Count);
	}

	[TestMethod]
	public void WithGroups_ShouldDisplayOwnerAndTags()
	{
		// Given two groups
		var groups = GenerateGroups(2).ToList();
		groupApiMock.Setup(api => api.GetOpenGroups(20)).ReturnsAsync(groups);

		// When the group list is rendered
		var groupList = context.RenderComponent<GroupList>();

		// Then for each group the owner and tags should be displayed
		var groupElements = groupList.FindAll(".group");
		Assert.IsTrue(groupElements[0].ToMarkup().Contains("Tag0"), "Tag0 missing in first group");
		Assert.IsTrue(groupElements[0].ToMarkup().Contains("Owner1"), "Owner1 not found");
		Assert.IsTrue(groupElements[1].ToMarkup().Contains("Owner2"), "Owner2 not found");
		Assert.IsTrue(groupElements[1].ToMarkup().Contains("Tag0"), "Tag0 missing in second group");
		Assert.IsTrue(groupElements[1].ToMarkup().Contains("Tag1"), "Tag1 missing in second group");
	}

	[TestCleanup]
	public void TearDown()
	{
		context.Dispose();
	}
}