using AutoFixture;
using ComicShelf.Models.User;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserController_PostTests : BaseTestController
	{
		[Fact]
		public async Task ShouldAddUser()
		{
			var expected = Fixture.Build<CreateUserDto>().With(t => t.Name, "test").Create();

			// Act
			var response = await Client.PostAsync("api/User/register", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializeObject = JsonConvert.DeserializeObject<UserDto>(jsonResult);
			var actual = Context.Users.Find(deserializeObject.Id);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.GoogleId, actual.GoogleId);
			Assert.Equal(expected.Email, actual.Email);
			Assert.Equal(expected.GivenName, actual.GivenName);
			Assert.Equal(expected.Picture, actual.Picture);
		}
	}
}
