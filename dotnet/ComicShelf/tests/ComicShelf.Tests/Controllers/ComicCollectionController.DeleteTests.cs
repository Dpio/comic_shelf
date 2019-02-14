using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Tests.Utilities;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicCollectionController_DeleteTests : BaseTestController
	{
		public async Task ShouldDeleteComicCollection()
		{
			var expected = Fixture.Build<ComicCollection>().With(t => t.Id, 1).Create();
			Context.ComicCollections.Add(expected);
			Context.SaveChanges();

			// Act

			var id = 1;
			var response = await Client.DeleteAsync($"api/ComicCollection/deleteFromCollection/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var check = await Client.GetAsync($"api/ComicCollection/getComicsCollection");
			Assert.Equal(HttpStatusCode.NotFound, check.StatusCode);
			var dbTest = Context.ComicCollections.FirstOrDefault(c => c.Id == id);
			Assert.Null(dbTest);
		}
	}
}

