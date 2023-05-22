namespace StudyMate.Server.User.Services;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Services;
using Shared.User.Services;

public class LoggedInAttribute : Attribute, IAsyncAuthorizationFilter
{
	public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
	{
		var token = context.HttpContext.Request.Headers[UserDataValidator.TokenKey];
		if (token.Count != 1 || !await context.HttpContext.RequestServices.GetRequiredService<IUserRepository>().IsTokenValid(Guid.Parse(token[0])))
		{
			context.Result = new UnauthorizedResult();
		}
	}
}
