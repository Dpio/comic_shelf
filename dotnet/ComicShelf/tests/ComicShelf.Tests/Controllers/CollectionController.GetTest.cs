using System;
using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Collection;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class CollectionController_GetTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetCollection()
		{
			var expected = Fixture.Build<Collection>().With(t => t.Id, 1).Create();
			Context.Collections.Add(expected);
			Context.SaveChanges();

			// Act
			var id = 1;
			var response = await Client.GetAsync($"api/Collection/{id}");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<CollectionDto>(jsonResult);
			Assert.Equal(expected.Id, actual.Id);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.Description, actual.Description);
			Assert.Equal(expected.IsPublic, actual.IsPublic);
		}
	}
}
