using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestEase;
using StudyMate.Client.AppShell;
using StudyMate.Client.Forum.Services;
using StudyMate.Client.Groups.Services;
using StudyMate.Client.User.Services;
using StudyMate.Shared.Services;
using StudyMate.Shared.Services.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<LocalStorage>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services
	.AddRestClientFor<IUserApi>()
	.AddRestClientFor<IGroupApi>()
	.AddRestClientFor<IForumApi>();

var host = builder.Build();
host.Services.GetRequiredService<AppState>().BuildVersion = Assembly.GetExecutingAssembly().GetBuildDate();
await host.RunAsync();

internal static class Extensions
{
	internal static IServiceCollection AddRestClientFor<TInterface>(this IServiceCollection services) where TInterface : class
		=> services.AddSingleton(sp => RestClient.For<TInterface>(sp.GetRequiredService<HttpClient>()));

	internal static string GetBuildDate(this Assembly assembly)
	{
		const string buildVersionMetadataPrefix = "+build";
		var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
		if (attribute?.InformationalVersion != null)
		{
			var value = attribute.InformationalVersion;
			var index = value.IndexOf(buildVersionMetadataPrefix, StringComparison.Ordinal);
			if (index > 0)
			{
				return value[(index + buildVersionMetadataPrefix.Length)..];
			}
		}

		return "<Error in GetBuildDate>";
	}
}