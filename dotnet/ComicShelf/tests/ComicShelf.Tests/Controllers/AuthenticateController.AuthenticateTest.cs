using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Authenticate;
using ComicShelf.Models.User;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class AuthenticateController : BaseTestController
	{
		[Fact]
		public async Task ShouldGetToken()
		{
			var expected = Fixture.Build<AuthenticateDto>().With(t => t.GoogleId, "string").With(t => t.Name, "string").Create();
			var user = Fixture.Build<User>().With(t => t.GoogleId, "string").With(t => t.Name, "string").Create();
			Context.Users.Add(user);
			Context.SaveChanges();


			// Act

			var response = await Client.PostAsync($"api/Authentication/authenticate", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<AuthenticateResponse>(jsonResult);
			Assert.Equal(user.GoogleId, actual.GoogleId);
			Assert.Equal(user.Name, actual.Name);
			Assert.Equal(user.GivenName, actual.GivenName);
			Assert.Equal(user.Picture, actual.Picture);
			Assert.NotEmpty(actual.Token);
		}
	}
}
