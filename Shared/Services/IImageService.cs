namespace StudyMate.Shared.Services;

using SixLabors.ImageSharp;

public interface IImageService
{
	byte[] ImageToByteArray(Image img);
	Image ByteArrayToImage(byte[] byteArray);
	string ImageSourcePath(byte[] byteArray);
}

