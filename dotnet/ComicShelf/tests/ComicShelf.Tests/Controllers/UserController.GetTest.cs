using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.User;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserController_GetTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetUser()
		{
			var expected = Fixture.Build<User>().With(t => t.Id, 1).Create();
			Context.Users.Add(expected);
			Context.SaveChanges();

			// Act

			var id = 1;
			var response = await Client.GetAsync($"api/User/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<UserDto>(jsonResult);
			Assert.Equal(expected.Id, actual.Id);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.GoogleId, actual.GoogleId);
			Assert.Equal(expected.Email, actual.Email);
			Assert.Equal(expected.GivenName, actual.GivenName);
			Assert.Equal(expected.Picture, actual.Picture);
		}
	}
}
