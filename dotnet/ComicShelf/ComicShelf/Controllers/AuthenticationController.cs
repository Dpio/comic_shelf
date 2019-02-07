using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using ComicShelf.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ComicShelf.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : Controller
	{
		private IAuthenticationService _authenticationService;
		private readonly AppSettings _appSettings;

		public AuthenticationController(
			IAuthenticationService authenticationService,
			IOptions<AppSettings> appSettings
			)
		{
			_authenticationService = authenticationService;
			_appSettings = appSettings.Value;
		}


		[HttpPost("authenticate")]
		[Produces("application/json", Type = typeof(AuthenticateResponse))]
		public IActionResult Authenticate([FromBody]AuthenticateDto authenticateDto)
		{
			var user = _authenticationService.Authenticate(authenticateDto.Name, authenticateDto.GoogleId);

			if (user == null)
				return BadRequest("Login failed");

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			// return basic user info (without password) and token to store client side
			return Ok(new AuthenticateResponse
			{
				GoogleId = user.GoogleId,
				Name = user.Name,
				GivenName = user.GivenName,
				Email = user.Email,
				Picture = user.Picture,
				Token = tokenString
			});
		}

		[HttpPost("signInWithGoogle")]
		public string AuthenticateWithGoogle()
		{
			return "https://accounts.google.com/o/oauth2/v2/auth?response_type=code&scope=https://www.googleapis.com/auth/plus.loginhttps://www.googleapis.com/auth/userinfo.emailhttps://www.googleapis.com/auth/plus.mehttps://www.googleapis.com/auth/userinfo.profile&access_type=offline&&redirect_uri=http://localhost:5001/getGoogleToken&client_id=654449040707-68osetc7oe7jgbrhi9gqs81abg1q6l72.apps.googleusercontent.com";
		}

		[HttpGet("getGoogleToken")]
		public async Task<IActionResult> GetGoogleToken(string code)
		{
			var user = await _authenticationService.GetAccessToken(code);
			//if (user == null)
			//	return BadRequest(new { message = "Login or password is incorrect" });

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Redirect($"http://localhost:4200/Logged/{user.Id}/{tokenString}");
		}
	}
}
