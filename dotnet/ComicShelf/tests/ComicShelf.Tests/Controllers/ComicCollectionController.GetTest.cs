using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.ComicCollection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicCollectionController_GetTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetComicCollection()
		{
			var id = Fixture.Create<int>();
			var comicCollections = Fixture.Build<ComicCollection>().CreateMany(5).OrderBy(p => p.Id).ToList();
			var comicCollection = Fixture.Build<ComicCollection>().With(t => t.UserId, id).Create();
			Context.ComicCollections.Add(comicCollection);
			Context.ComicCollections.AddRange(comicCollections);
			Context.SaveChanges();

			// Act

			var response = await Client.GetAsync($"api/ComicCollection/getComicsCollection/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<IEnumerable<ComicCollectionDto>>(jsonResult).ToList();
			Assert.Contains(actual, dto => dto.UserId == comicCollection.UserId);
			Assert.Equal(1, actual: actual.Count);
		}
	}
}
