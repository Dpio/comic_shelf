using AutoFixture;
using ComicShelf.DataAccess.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserCollectionController_DeleteTests : BaseTestController
	{
		[Fact]
		public async Task ShouldDeleteUserCollection()
		{
			var expected = Fixture.Build<UserCollection>().With(t => t.Id, 1).Create();
			Context.UserCollections.Add(expected);
			Context.SaveChanges();

			// Act

			var id = 1;
			var response = await Client.DeleteAsync($"api/UserCollection/deleteCollectionFromUserCollection/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var dbTest = Context.UserCollections.FirstOrDefault(c => c.Id == id);
			Assert.Null(dbTest);
		}
	}
}

