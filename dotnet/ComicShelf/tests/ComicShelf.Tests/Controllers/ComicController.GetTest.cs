using AutoFixture;
using ComicShelf.Api.Controllers;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Authenticate;
using ComicShelf.Models.Comic;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicController_GetTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetComic()
		{
			var expected = Fixture.Build<Comic>().With(t => t.Id, 1).Create();
			Context.Comics.Add(expected);
			Context.SaveChanges();

			// Act
			
			var id = 1;
			var response = await Client.GetAsync($"api/Comic/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<ComicDto>(jsonResult);
			Assert.Equal(expected.Id, actual.Id);
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
