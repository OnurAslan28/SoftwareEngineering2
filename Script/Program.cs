namespace StudyMate.Script;

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StudyMate.Data;
using StudyMate.Data.Entities;
using StudyMate.Data.Services;
using StudyMate.Shared.Services;
using StudyMate.Shared.Services.Implementations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
	// prod
	// const string conString = "Host=localhost;Port=5432;Database=postgresdb;Persist Security Info=True;User ID=postgresadmin;Password=Nlgjds72gS";

	// local
	// const string conString = "";

	static async Task Main(string[] args)
	{
		#region setting up db
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		var optionsBuilder = new DbContextOptionsBuilder<StudyMateContext>();
		optionsBuilder.UseNpgsql(conString)
				.EnableSensitiveDataLogging();
		var context = new StudyMateContext(optionsBuilder.Options);
		#endregion

		Console.WriteLine("⚐ ------- starting scripts ------- ⚐");

		IImageService imageService = new ImageService();
		await UploadDefaultImages(context, imageService);

		Console.WriteLine("⚑ ------- finished scripts ------- ⚑");
	}


	private static async Task UploadDefaultImages(StudyMateContext context, IImageService imageService)
	{
		#region creating default profile pictures
		var yellow = imageService.ImageToByteArray(Image.Load("Script/Data/TestImages/yellow_avatar.jpeg"));
		var blue = imageService.ImageToByteArray(Image.Load("Script/Data/TestImages/blue_avatar.jpeg"));
		var brown = imageService.ImageToByteArray(Image.Load("Script/Data/TestImages/brown_avatar.jpeg"));
		var red = imageService.ImageToByteArray(Image.Load("Script/Data/TestImages/red_avatar.jpeg"));
		var green = imageService.ImageToByteArray(Image.Load("Script/Data/TestImages/green_avatar.jpeg"));
		var purple = imageService.ImageToByteArray(Image.Load("Script/Data/TestImages/purple_avatar.jpeg"));

		var pictures = new ProfilePicture[]
		{
			 	new ProfilePicture{ImageData=yellow, ForDefault=true},
			 	new ProfilePicture{ImageData=blue, ForDefault=true},
			 	new ProfilePicture{ImageData=brown, ForDefault=true},
			 	new ProfilePicture{ImageData=red, ForDefault=true},
			 	new ProfilePicture{ImageData=green, ForDefault=true},
			 	new ProfilePicture{ImageData=purple, ForDefault=true}
		};
		context.ProfilePictures.AddRange(pictures);

		await context.SaveChangesAsync();
		#endregion
	}
}