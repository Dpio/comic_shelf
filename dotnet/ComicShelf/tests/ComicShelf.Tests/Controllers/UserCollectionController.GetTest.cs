using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.UserCollection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserCollectionController_GetTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetUserCollection()
		{
			var comicCollection = Fixture.Build<ComicCollection>().Create();
			var collection = Fixture.Build<Collection>().Create();
			Context.ComicCollections.Add(comicCollection);
			Context.Collections.Add(collection);
			Context.SaveChanges();
			var userCollections = Fixture.Build<UserCollection>().With(c => c.ComicCollectionId, comicCollection.Id).CreateMany(5).OrderBy(p => p.Id).ToList();
			var userCollection = Fixture.Build<UserCollection>().With(t => t.CollectionId, collection.Id).With(t => t.ComicCollectionId, comicCollection.Id).CreateMany(2);
			Context.UserCollections.AddRange(userCollection);
			Context.UserCollections.AddRange(userCollections);
			Context.SaveChanges();

			// Act
			var response = await Client.GetAsync($"api/UserCollection/getUserCollection/{collection.Id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<IEnumerable<UserCollectionDto>>(jsonResult).ToList();
			Assert.Contains(actual, dto => dto.CollectionId == collection.Id);
			Assert.Equal(2, actual: actual.Count);
		}
	}
}
