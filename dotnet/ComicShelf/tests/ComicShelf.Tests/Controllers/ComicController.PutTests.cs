using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Comic;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicController_PutTests : BaseTestController
	{
		[Fact]
		public async Task ShouldUpdateComic()
		{
			var expected = Fixture.Build<Comic>().With(t => t.Id, 1).Create();
			Context.Comics.Add(expected);
			Context.SaveChanges();

			// Act
			expected.Title = "New Title";
			expected.Publisher = "New Publisher";
			expected.Issue = "New Issue";
			expected.Note = "New Note";
			expected.Image = 1;
			expected.StartDate = DateTime.Now;
			expected.EndDate = DateTime.Now;
			var response = await Client.PutAsync($"api/Comic", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializedResponse = JsonConvert.DeserializeObject<ComicDto>(jsonResult);
			var actual = Context.Comics.Find(deserializedResponse.Id);
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
