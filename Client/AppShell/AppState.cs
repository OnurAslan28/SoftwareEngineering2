namespace StudyMate.Client.AppShell;

using Shared.Models;
using Shared.User.Services;
using User.Services;

public class AppState
{
	private readonly HttpClient httpClient;
	private readonly LocalStorage localStorage;

	public string BuildVersion = "";

	/// <summary>
	/// Can be awaited to make sure initialization is finished.
	/// </summary>
	public readonly Task InitTask;

	public event Action? LoginChanged;
	private User? currentUser;

	/// <summary>
	/// The currently logged in user.
	/// </summary>
	public User? CurrentUser
	{
		get => currentUser;
		private set
		{
			currentUser = value;
			LoginChanged?.Invoke();
		}
	}

	public AppState(HttpClient httpClient, LocalStorage localStorage, IUserApi userApi)
	{
		this.httpClient = httpClient;
		this.localStorage = localStorage;
		InitTask = LoadExistingLogin(userApi);
	}

	private async Task LoadExistingLogin(IUserApi userApi)
	{
		var token = await localStorage.GetGuid(UserDataValidator.TokenKey);
		if (!token.HasValue)
		{
			_ =localStorage.Remove(UserDataValidator.TokenKey);
			return;
		}
		
		var savedLogin = await userApi.Valdiate(token.Value);
		if (!savedLogin.Success || savedLogin.UserData is null)
		{
			_ = localStorage.Remove(UserDataValidator.TokenKey);
			return;
		}

		await SetLoggedIn(savedLogin.UserData, token.Value, true);
	}

	/// <summary>
	/// Sets the app state to logged in.
	/// </summary>
	/// <param name="user">The logged in user.</param>
	/// <param name="token">The token to authorize the user with.</param>
	/// <param name="persistent">Whether or not to save the login permanently across sessions.</param>
	/// <returns>A task representing the operation, finishing when the login finishes.</returns>
	public async Task SetLoggedIn(User user, Guid token, bool persistent)
	{
		httpClient.DefaultRequestHeaders.Add(UserDataValidator.TokenKey, token.ToString());
		if (persistent) await localStorage.Set(UserDataValidator.TokenKey, token);
		CurrentUser = user;
	}

	public async Task SetLoggedOut()
	{
		httpClient.DefaultRequestHeaders.Remove(UserDataValidator.TokenKey);
		await localStorage.Remove(UserDataValidator.TokenKey);
		CurrentUser = null;
	}
}