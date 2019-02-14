using AutoFixture;
using ComicShelf.Models.ComicCollection;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicCollectionController_PostTests : BaseTestController
	{
		[Fact]
		public async Task ShouldAddComicCollection()
		{
			var expected = Fixture.Build<CreateComicCollectionDto>().Create();


			// Act
			var response = await Client.PostAsync("api/ComicCollection/addToCollection", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializeObject = JsonConvert.DeserializeObject<ComicCollectionDto>(jsonResult);
			var actual = Context.ComicCollections.Find(deserializeObject.Id);
			Assert.Equal(expected.UserId, actual.UserId);
			Assert.Equal(expected.ComicId, actual.ComicId);
		}
	}
}
