namespace StudyMate.Shared.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;

public class LoginResult
{
	public bool Success { get; set; }

	/// <summary>
	/// Only set if the login was successful
	/// </summary>
	public Guid? Token { get; set; }

	/// <summary>
	/// Only set if the login was successful
	/// </summary>
	public User? UserData { get; set; }

	/// <summary>
	/// Only set if the login failed
	/// </summary>
	public string? ErrorMessage { get; set; }
}
