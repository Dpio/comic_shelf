using AutoFixture;
using ComicShelf.DataAccess.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class CollectionController_DeleteTests : BaseTestController
	{
		[Fact]
		public async Task ShouldDeleteCollection()
		{
			var expected = Fixture.Build<Collection>().With(t => t.Id, 1).Create();
			Context.Collections.Add(expected);
			Context.SaveChanges();

			// Act

			var id = 1;
			var response = await Client.DeleteAsync($"api/Collection/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var check = await Client.GetAsync($"api/Collection/{id}");
			Assert.Equal(HttpStatusCode.NotFound, check.StatusCode);
			var dbTest = Context.Collections.FirstOrDefault(c => c.Id == id);
			Assert.Null(dbTest);
		}
	}
}
