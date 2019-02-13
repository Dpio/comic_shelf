using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ComicShelf.Tests.Controllers
{
	public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
	{
		public TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var authenticationTicket = new AuthenticationTicket(
				new System.Security.Claims.ClaimsPrincipal(Options.Identity),
				new AuthenticationProperties(),
				"Test Scheme");
			return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
		}
	}
	
}
