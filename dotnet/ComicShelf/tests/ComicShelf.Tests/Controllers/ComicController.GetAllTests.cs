using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Comic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicController_GetAllTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetAllComics()
		{
			string Title = String.Empty;
			var comics = Fixture.Build<Comic>().CreateMany(5).OrderBy(p => p.Id).ToList();
			var comic = Fixture.Build<Comic>().With(t => t.Title, Title).Create();
			Context.Comics.Add(comic);
			Context.Comics.AddRange(comics);
			Context.SaveChanges();

			// Act
			var response = await Client.GetAsync("api/Comic/getAll");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<IEnumerable<ComicDto>>(jsonResult).ToList();
			Assert.Contains(actual, dto => dto.Title == comic.Title);
			Assert.Equal(6, actual: actual.Count);
		}
	}
}
