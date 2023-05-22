namespace StudyMate.Data.Repositories;

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Shared.User.Models;
using Services;
using StudyMate.Shared.Services;
using User = Shared.Models.User;

public class UserRepository : IUserRepository
{
	private readonly StudyMateContext _context;
	private readonly CryptoService _cryptoService = new CryptoService();

	public UserRepository(StudyMateContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<User?> FindAsync(string username)
	{
		var userEntity = await _context.Users
			.Include(u => u.ProfilePicture)
			.SingleOrDefaultAsync(u => u.Username == username);

		if (userEntity is null)
		{
			return null;
		}
		else
		{
			return new User(
				userEntity.Username,
				userEntity.ProfilePicture.ImageData,
				userEntity.BioInfo,
				userEntity.CourseOfStudy
			);
		}
	}

	public async Task UpdateAsync(User user)
	{
		var userEntity = await _context.Users
			.Include(u => u.ProfilePicture)
			.FirstOrDefaultAsync(u => u.Username == user.Username);

		if (userEntity is null)
			throw new ArgumentNullException($"User {user.Username} not found");

		userEntity.ProfilePicture.ImageData = user.ProfilePicture;
		userEntity.BioInfo = user.BioInfo;
		userEntity.CourseOfStudy = user.CourseOfStudy;

		await _context.SaveChangesAsync();
	}

	public async Task<bool> UpdatePasswordSuccess(string username, string currentPassword, string newPassword)
	{
		var userEntity = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

		if (userEntity is null)
		{
			throw new ArgumentNullException($"User {username} not found while checking the password");
		}
		else
		{
			if (userEntity.Password.SequenceEqual(_cryptoService.Hash(currentPassword)))
			{
				userEntity.Password = _cryptoService.Hash(newPassword);
				await _context.SaveChangesAsync();
				return true;
			}

			return false;
		}
	}

	public async Task<LoginResult> Login(string username, string password)
	{
		var userEntity = await _context.Users.Include(u => u.ProfilePicture).SingleOrDefaultAsync(u => u.Username == username);
		if (userEntity is null)
		{
			return new LoginResult
			{
				Success = false,
				ErrorMessage = "User not found"
			};
		}


		if (userEntity.Password.SequenceEqual(_cryptoService.Hash(password)))
		{
			var tokenEntity = new LoginToken
			{
				Created = DateTime.Now,
				UserNavigation = userEntity,
				Token = Guid.NewGuid(),
				Username = username
			};
			_context.LoginTokens.Add(tokenEntity);
			await _context.SaveChangesAsync();

			return new LoginResult
			{
				Success = true,
				UserData = userEntity.ToModel(),
				Token = tokenEntity.Token
			};
		}

		return new LoginResult
		{
			Success = false,
			ErrorMessage = "Password is incorrect"
		};
	}

	public async Task<LoginResult> Register(string username, string password)
	{
		if (await _context.Users.AnyAsync(u => u.Username == username))
		{
			return new LoginResult
			{
				Success = false,
				ErrorMessage = "Username already taken"
			};
		}
		
		var defaultPictures = await _context.ProfilePictures.Where(p => p.ForDefault).ToListAsync();
		var user = new Entities.User
		{
			Username = username,
			Password = _cryptoService.Hash(password),
			ProfilePicture = defaultPictures.ElementAt(new Random().Next(defaultPictures.Count))
		};
		_context.Users.Add(user);
		await _context.SaveChangesAsync();
		return new LoginResult
		{
			Success = true,
			UserData = user.ToModel()
		};
	}

	public async Task<LoginResult> ValidateLogin(Guid token)
	{
		var tokenEntity = await _context.LoginTokens.SingleOrDefaultAsync(t => t.Token == token);
		if (tokenEntity is null)
		{
			return new LoginResult
			{
				Success = false,
				ErrorMessage = "Invalid token"
			};
		}

		var userEntity = await _context.Users.Include(u => u.ProfilePicture).SingleOrDefaultAsync(u => u.Username == tokenEntity.Username);
		if (userEntity is null)
		{
			return new LoginResult
			{
				Success = false,
				ErrorMessage = "User not found"
			};
		}

		return new LoginResult
		{
			Success = true,
			UserData = userEntity.ToModel()
		};
	}

	public async Task<bool> IsTokenValid(Guid token)
	{
		return await _context.LoginTokens.AnyAsync(t => t.Token == token);
	}

	public async Task RemoveLogin(Guid token)
	{
		var tokenEntity = await _context.LoginTokens.SingleOrDefaultAsync(t => t.Token == token);
		if (tokenEntity is null)
		{
			return;
		}

		_context.LoginTokens.Remove(tokenEntity);
		await _context.SaveChangesAsync();
	}
}