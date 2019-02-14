using AutoFixture;
using ComicShelf.Models.Collection;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class CollectionController_PostTests : BaseTestController
	{
		[Fact]
		public async Task ShouldAddCollection()
		{
			var expected = Fixture.Build<CreateCollectionDto>().With(t => t.Name, "test").Create();

			// Act
			var response = await Client.PostAsync("api/Collection", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializeObject = JsonConvert.DeserializeObject<CollectionDto>(jsonResult);
			var actual = Context.Collections.Find(deserializeObject.Id);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.Description, actual.Description);
			Assert.Equal(expected.IsPublic, actual.IsPublic);
		}
	}
}
