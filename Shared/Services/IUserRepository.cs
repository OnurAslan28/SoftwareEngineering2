namespace StudyMate.Shared.Services;

using Models;
using User.Models;

public interface IUserRepository
{
	public Task<User?> FindAsync(string username);
	public Task UpdateAsync(User user);
	public Task<bool> UpdatePasswordSuccess(string username, string currentPassword, string newPassword);

	/// <summary>
	/// Creates a new token for the user.
	/// </summary>
	/// <param name="username">ID of the user to log in.</param>
	/// <param name="password">Plain text password of the user to log in.</param>
	/// <returns>An object containing either both user and token on success, or an error message on failure.</returns>
	Task<LoginResult> Login(string username, string password);

	/// <summary>
	/// Registers a new user.
	/// </summary>
	/// <param name="username">The created user will have this username</param>
	/// <param name="password">This password will be used for this user</param>
	/// <returns>Either the created user or an error message</returns>
	Task<LoginResult> Register(string username, string password);

	/// <summary>
	/// Validates the passed token to renew a login.
	/// </summary>
	/// <param name="token">The login token to validate.</param>
	/// <returns>An object containing either the logged in user or an error message.</returns>
	Task<LoginResult> ValidateLogin(Guid token);

	/// <summary>
	/// Checks whether or not a token is valid.
	/// </summary>
	/// <param name="token">A guid representing the token.</param>
	/// <returns>A bool indicating whether or not the token is valid.</returns>
	Task<bool> IsTokenValid(Guid token);

	/// <summary>
	/// Removes a login token.
	/// </summary>
	/// <param name="token">The token to remove from the logins table.</param>
	/// <returns>A task representing the operation, finishing when the operation finishes.</returns>
	Task RemoveLogin(Guid token);
}