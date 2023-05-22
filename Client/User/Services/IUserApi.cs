namespace StudyMate.Client.User.Services;

using RestEase;
using Shared.Models;
using Shared.User.Models;
using Components;
using Shared.User.Services;

[BasePath("api/user")]
public interface IUserApi
{
	[Get]
	public Task<User?> FindAsync([Query] string username);

	[Put]
	public Task UpdateAsync([Body] User user);

	[Post("password")]
	public Task<bool> UpdatePasswordSuccess([Body] PasswordUpdateRequest request);
	
	[Post("login")]
	public Task<LoginResult> Login([Body] LoginRequest request);

	[Post("register")]
	public Task<LoginResult> Register([Body] RegistrationRequest request);

	[Get("login")]
	public Task<LoginResult> Valdiate([Header(UserDataValidator.TokenKey)]Guid token);

	[Delete("login")]
	public Task Logout();
}