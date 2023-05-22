namespace StudyMate.Client.User.Services;

using Microsoft.JSInterop;

public class LocalStorage
{
	private readonly IJSRuntime jSRuntime;

	public LocalStorage(IJSRuntime jSRuntime)
	{
		this.jSRuntime = jSRuntime;
	}

	internal async Task Set<T>(string key, T value)
	{
		await jSRuntime.InvokeVoidAsync("localStorage.setItem", key, value!);
	}

	internal async Task<T?> Get<T>(string key) where T : class
	{
		return await jSRuntime.InvokeAsync<T?>("localStorage.getItem", key);
	}

	internal async Task Remove(string key)
	{
		await jSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
	}

	internal async Task<Guid?> GetGuid(string key)
	{
		var stringData = await Get<string>(key);
		return Guid.TryParse(stringData, out var data)
			? data
			: null;
	}
}