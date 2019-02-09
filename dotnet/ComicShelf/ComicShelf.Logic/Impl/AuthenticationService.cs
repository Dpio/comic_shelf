using ComicShelf.DataAccess.Entities;
using ComicShelf.Logic.Helpers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComicShelf.Logic.Impl
{
	public class AuthenticationService : IAuthenticationService
	{
		private IUserService _userService;

		public AuthenticationService(IUserService userService)
		{
			_userService = userService;
		}


		public User Authenticate(string Name, string GoogleId)
		{
			if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(GoogleId))
				return null;

			var user = _userService.GetAll().FirstOrDefault(x => x.Name == Name);

			// check if username exists
			if (user == null)
				return null;

			// authentication successful
			return user;
		}

		public async Task<User> GetAccessToken(string code)
		{
			string clientId = "654449040707-68osetc7oe7jgbrhi9gqs81abg1q6l72.apps.googleusercontent.com";
			string clientSecret = "PnMvDLoP4qQg_Rnti0TmfpH8";
			string redirect_uri = "https://localhost:5001/api/Authentication/getGoogleToken";
			var content = new StringContent
			(
				"code=" + code
				+ "&client_id=" + clientId
				+ "&client_secret=" + clientSecret
				+ "&redirect_uri=" + redirect_uri
				+ "&grant_type=" + "authorization_code"
			);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
			string accessToken = string.Empty;

			using (var client = new HttpClient())
			{
				HttpResponseMessage response = null;
				response = await client.PostAsync("https://www.googleapis.com/oauth2/v3/token", content);

				if (response.StatusCode == HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsStringAsync();
					GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(result);

					if (serStatus != null)
					{
						accessToken = serStatus.access_token;
					}
				}
			}
			if (!String.IsNullOrWhiteSpace(accessToken))
			{
				User user = await GetGooglePlusUserData(accessToken);
				return user;
			}

			return null;
		}

		private async Task<User> GetGooglePlusUserData(string access_token)
		{
			try
			{
				var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;
				User user = null;
				string serStatusId = null;
				using (HttpClient client = new HttpClient())
				{
					client.CancelPendingRequests();
					HttpResponseMessage output = await client.GetAsync(urlProfile);
					if (output.IsSuccessStatusCode)
					{
						string outputData = await output.Content.ReadAsStringAsync();
						GoogleUserOutputData serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(outputData);

						if (serStatus != null)
						{
							user = new User()
							{
								GoogleId = serStatus.id,
								Name = serStatus.name,
								GivenName = serStatus.given_name,
								Email = serStatus.email,
								Picture = serStatus.picture
							};
							serStatusId = serStatus.id;
						}
					}
				}

				if (user != null)
				{
					var users = _userService.GetAll();
					if (users.Any(x => x.Name == user.Name))
					{
						var auth = Authenticate(user.Name, serStatusId);
						return auth;
					}
					else
					{
						var create = _userService.Create(user);
						return create;
					}
				}
			}
			catch (Exception ex)
			{
				throw new AppException(ex.Message);
			}
			return null;
		}
	}
}
