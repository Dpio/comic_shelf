using AutoFixture;
using ComicShelf.DataAccess.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class ComicController_DeleteTests : BaseTestController
	{
		[Fact]
		public async Task ShouldDeleteComic()
		{
			var expected = Fixture.Build<Comic>().With(t => t.Id, 1).Create();
			Context.Comics.Add(expected);
			Context.SaveChanges();

			// Act

			var id = 1;
			var response = await Client.DeleteAsync($"api/Comic/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var check = await Client.GetAsync($"api/Comic/{id}");
			Assert.Equal(HttpStatusCode.NotFound, check.StatusCode);
			var dbTest = Context.Comics.FirstOrDefault(c => c.Id == id);
			Assert.Null(dbTest);
		}
	}
}
