namespace StudyMate.Server.User.Facade;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Models;
using Shared.Services;
using Shared.User.Services;
using StudyMate.Shared.User.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserRepository _userRepo;

	public UserController(IUserRepository userRepo)
	{
		_userRepo = userRepo;
	}

	[HttpGet]
	public async Task<User?> FindAsync([FromQuery] string username)
	{
		return await _userRepo.FindAsync(username);
	}

	[HttpPut]
	[LoggedIn]
	public async Task UpdateAsync(User user)
	{
		await _userRepo.UpdateAsync(user);
	}

	[HttpPost("password")]
	[LoggedIn]
	public async Task<bool> UpdatePasswordSuccess(PasswordUpdateRequest request)
	{
		return await _userRepo.UpdatePasswordSuccess(request.Username, request.CurrentPassword, request.NewPassword);
	}

	[HttpPost("login")]
	public async Task<LoginResult> Login(LoginRequest request)
	{
		return await _userRepo.Login(request.Username, request.Password);
	}

	[HttpPost("register")]
	public async Task<LoginResult> Register(RegistrationRequest request)
	{
		Validator.ValidateObject(request, new ValidationContext(request), true);
		var result = await _userRepo.Register(request.Username, request.Password);

		if (result.Success && request.AutoLogin)
		{
			result = await _userRepo.Login(request.Username, request.Password);
		}

		return result;
	}

	[HttpGet("login")]
	public async Task<LoginResult> Login([FromHeader(Name = UserDataValidator.TokenKey)] Guid token)
	{
		return await _userRepo.ValidateLogin(token);
	}

	[HttpDelete("login")]
	[LoggedIn]
	public async void Logout([FromHeader(Name = UserDataValidator.TokenKey)] Guid token)
	{
		try
		{
			await _userRepo.RemoveLogin(token);
		}
		catch (Exception e)
		{
			Console.WriteLine($"Logout failed: {e.Message}");
			// Fuck it, logout is fire-and-forget, client will be fine
		}
	}
}