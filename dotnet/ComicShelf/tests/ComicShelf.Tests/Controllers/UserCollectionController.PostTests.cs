using AutoFixture;
using ComicShelf.Models.UserCollection;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserCollectionController_PostTests : BaseTestController
	{
		[Fact]
		public async Task ShouldAddUserCollection()
		{
			var expected = Fixture.Build<CreateUserCollectionDto>().Create();

			// Act
			var response = await Client.PostAsync("api/UserCollection/addToUserCollection", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializeObject = JsonConvert.DeserializeObject<UserCollectionDto>(jsonResult);
			var actual = Context.UserCollections.Find(deserializeObject.Id);
			Assert.Equal(expected.CollectionId, actual.CollectionId);
			Assert.Equal(expected.ComicCollectionId, actual.ComicCollectionId);
		}
	}
}
