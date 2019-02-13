using AutoFixture;
using ComicShelf.Models.Comic;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicController_PostTests : BaseTestController
	{
		[Fact]
		public async Task ShouldAddComic()
		{
			var expected = Fixture.Build<CreateComicDto>().With(t => t.Title, "test").Create();

			// Act
			var response = await Client.PostAsync("api/Comic", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializeObject = JsonConvert.DeserializeObject<ComicDto>(jsonResult);
			var actual = Context.Comics.Find(deserializeObject.Id);
			Assert.Equal(expected.Title, actual.Title);
			Assert.Equal(expected.Publisher, actual.Publisher);
			Assert.Equal(expected.Issue, actual.Issue);
			Assert.Equal(expected.Image, actual.Image);
			Assert.Equal(expected.Note, actual.Note);
			Assert.Equal(expected.StartDate, actual.StartDate);
			Assert.Equal(expected.EndDate, actual.EndDate);
		}
	}
}
