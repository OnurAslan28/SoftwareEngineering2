namespace StudyMate.Client.Test.Forum.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using Client.Forum.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudyMate.Client.Forum.Services;
using Shared.Models;
using TestContext = Bunit.TestContext;

[TestClass]
public class ForumPostFeedTest
{
    private TestContext context = null!;
    private Mock<IForumApi> forumApiMock = null!;

    [TestInitialize]
    public void Setup()
    {
        context = new();
        forumApiMock = new(MockBehavior.Strict);
        context.Services.AddSingleton(forumApiMock.Object);
    }

    private static IEnumerable<Post> GeneratePosts(int numberOfPosts)
    {
        for (uint i = 1; i <= numberOfPosts; i++)
        {
            yield return new($"Post {i}", false, $"I'm generated post number {i}!", DateTime.Today.AddHours(i+10), new($"Forum {i%2}"), new($"User {i}", new byte[5]), new List<string>());
        }
    }

    [TestMethod]
    public void WithPosts_ShouldDisplayPosts()
    {
        // Given 5 posts
        const int numOfPosts = 5;
        var posts = GeneratePosts(numOfPosts).ToList();
        forumApiMock.Setup(api => api.GetPosts(20)).ReturnsAsync(posts);

        // When the feed is rendered
        var feed = context.RenderComponent<ForumPostFeed>();

        // Then 5 posts should be visible
        Assert.AreEqual(5, feed.FindAll(".post").Count, "Incorrect number of posts rendered.");
    }

    [TestMethod]
    public void WithoutPosts_ShouldDisplayNoPosts()
    {
        // Given there are no posts
        forumApiMock.Setup(api => api.GetPosts(20)).ReturnsAsync(new List<Post>(0));

        // When the feed is rendered
        var feed = context.RenderComponent<ForumPostFeed>();

        // Then it should display no posts
        Assert.AreEqual(0, feed.FindAll(".post").Count, "Incorrect number of posts rendered.");
    }

    [TestMethod]
    public void WithGroups_ShouldDisplayOwnerAndTags()
    {
        // Given two groups
        var groups = GeneratePosts(2).ToList();
        forumApiMock.Setup(api => api.GetPosts(20)).ReturnsAsync(groups);

        // When the group list is rendered
        var feed = context.RenderComponent<ForumPostFeed>();

        // Then for each group the owner and tags should be displayed
        var posts = feed.FindAll(".post");
        Assert.IsTrue(posts[0].ToMarkup().Contains("Post 1"), "Post 1 not found");
        Assert.IsTrue(posts[0].ToMarkup().Contains("I'm generated post number 1!"), "No content in post 1");
        Assert.IsTrue(posts[1].ToMarkup().Contains("Post 2"), "Post 2 not found");
        Assert.IsTrue(posts[1].ToMarkup().Contains("I'm generated post number 2!"), "No content in post 2");
    }

    [TestCleanup]
    public void TearDown()
    {
        context.Dispose();
    }
}
