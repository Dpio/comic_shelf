using AutoFixture;
using ComicShelf.DataAccess.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserController_DeleteTests : BaseTestController
	{
		[Fact]
		public async Task ShouldDeleteUser()
		{
			var expected = Fixture.Build<User>().With(t => t.Id, 1).Create();
			Context.Users.Add(expected);
			Context.SaveChanges();

			// Act

			var id = 1;
			var response = await Client.DeleteAsync($"api/User/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var check = await Client.GetAsync($"api/User/{id}");
			Assert.Equal(HttpStatusCode.NoContent, check.StatusCode);
			var dbTest = Context.Users.FirstOrDefault(c => c.Id == id);
			Assert.Null(dbTest);
		}
	}
}
