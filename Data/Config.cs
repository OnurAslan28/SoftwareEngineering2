namespace StudyMate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Config
{
	public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
#if DEBUG
		services.AddDbContextFactory<StudyMateContext>(opt =>
				opt.UseNpgsql(config.GetConnectionString("StudyMateContext"))
				.EnableSensitiveDataLogging());
#else
		services.AddDbContextFactory<StudyMateContext>(opt =>
			opt.UseNpgsql(config.GetConnectionString("StudyMateContext")));
#endif
		return services;
	}

	public static void UpdateDatabase(this IServiceProvider provider)
	{
		using var scope = provider.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<StudyMateContext>();
		db.Database.Migrate();

	}
}