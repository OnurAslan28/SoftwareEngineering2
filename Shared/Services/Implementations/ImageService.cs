namespace StudyMate.Shared.Services.Implementations;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;

public class ImageService : IImageService
{
	public byte[] ImageToByteArray(Image img)
	{
		using (MemoryStream mStream = new MemoryStream())
		{
			var imageEncoder = img.GetConfiguration().ImageFormatsManager.FindEncoder(JpegFormat.Instance);
			img.Save(mStream, imageEncoder);
			return mStream.ToArray();
		}
	}

	public Image ByteArrayToImage(byte[] byteArray)
	{
		return Image.Load<Rgba32>(byteArray);
	}

	public string ImageSourcePath(byte[] byteArray)
	{
		var base64 = Convert.ToBase64String(byteArray);
		return string.Format("data:image/jpeg;base64,{0}", base64);
	}
}