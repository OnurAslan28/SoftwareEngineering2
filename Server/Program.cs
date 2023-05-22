using Microsoft.OpenApi.Models;
using StudyMate.Data;
using StudyMate.Data.Repositories;
using StudyMate.Shared.Services;
using StudyMate.Shared.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyMate", Version = "v1" }));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ISubForumRepository, SubForumRepository>();
builder.Services.AddSingleton<IImageService, ImageService>();

// Add configurations
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.Sources.Clear();
	config.AddEnvironmentVariables("ASPNETCORE_");

	var env = hostingContext.HostingEnvironment;

	config.SetBasePath(env.ContentRootPath)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
			.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
			.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) //load environment settings
			.AddEnvironmentVariables();

	if (args != null)
	{
		config.AddCommandLine(args);
	}
});

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.Services.UpdateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseSwagger()
	.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyMate"));

app.Run();
