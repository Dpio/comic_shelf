using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.User;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserController_PutTests : BaseTestController
	{
		[Fact]
		public async Task ShouldUpdateUser()
		{
			var expected = Fixture.Build<User>().With(t => t.Id, 1).Create();
			Context.Users.Add(expected);
			Context.SaveChanges();

			// Act
			expected.Name = "New Name";
			expected.GoogleId = "New GoogleId";
			expected.Email = "New Email";
			expected.GivenName = "New GivenName";
			expected.Picture = "New Picture";
			var response = await Client.PutAsync($"api/User", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializedResponse = JsonConvert.DeserializeObject<UserDto>(jsonResult);
			var actual = Context.Users.Find(deserializedResponse.Id);
			Assert.Equal(expected.Id, actual.Id);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.GoogleId, actual.GoogleId);
			Assert.Equal(expected.Email, actual.Email);
			Assert.Equal(expected.GivenName, actual.GivenName);
			Assert.Equal(expected.Picture, actual.Picture);
		}
	}
}
