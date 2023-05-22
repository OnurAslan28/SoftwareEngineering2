namespace StudyMate.Data.Services;

using System.Security.Cryptography;
using System.Text;

public class CryptoService
{
	public byte[] Hash(string password)
	{
		using var function = SHA256.Create();
		var clearBytes = Encoding.UTF8.GetBytes(password);
		var hashedBytes = function.ComputeHash(clearBytes);
		return hashedBytes;
	}
}
