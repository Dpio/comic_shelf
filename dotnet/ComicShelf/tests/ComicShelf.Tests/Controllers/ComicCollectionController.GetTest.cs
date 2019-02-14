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
		    var user = Fixture.Build<User>().Create();
            var comics = Fixture.Build<Comic>().Create();
            Context.Users.Add(user);
            Context.Comics.Add(comics);
		    Context.SaveChanges();
		    var comicCollections = Fixture.Build<ComicCollection>().With(c => c.ComicId, comics.Id).CreateMany(5).OrderBy(p => p.Id).ToList();
			var comicCollection = Fixture.Build<ComicCollection>().With(t => t.UserId, user.Id).With(t => t.ComicId, comics.Id).Create();
			Context.ComicCollections.Add(comicCollection);
			Context.ComicCollections.AddRange(comicCollections);
			Context.SaveChanges();

			// Act
			var response = await Client.GetAsync($"api/ComicCollection/getComicsCollection/{user.Id}");
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
