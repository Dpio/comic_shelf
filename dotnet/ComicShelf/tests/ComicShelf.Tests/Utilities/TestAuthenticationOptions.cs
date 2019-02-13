using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;

namespace ComicShelf.Tests.Controllers
{
	public class TestAuthenticationOptions : AuthenticationSchemeOptions
	{
		public virtual ClaimsIdentity Identity { get; } = new ClaimsIdentity(new Claim[]
		{
			new Claim("http://schemas.xmlsoap.org.ws/2005/05/identity/claims/nameidentifer", Guid.NewGuid().ToString()),
		}, "test"
			);
	}
}
