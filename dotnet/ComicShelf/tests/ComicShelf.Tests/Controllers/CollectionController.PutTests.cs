using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Comic;
using ComicShelf.Tests.Utilities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class CollectionController_PutTests : BaseTestController
	{
		[Fact]
		public async Task ShouldUpdateCollection()
		{
			var expected = Fixture.Build<Collection>().With(t => t.Id, 1).Create();
			Context.Collections.Add(expected);
			Context.SaveChanges();

			// Act
			expected.Name = "New Name";
			expected.Description = "New Description";
			expected.IsPublic = false;
			var response = await Client.PutAsync($"api/Collection", new JsonContent(expected));
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var deserializedResponse = JsonConvert.DeserializeObject<ComicDto>(jsonResult);
			var actual = Context.Collections.Find(deserializedResponse.Id);
			Assert.Equal(expected.Id, actual.Id);
			Assert.Equal(expected.Name, actual.Name);
			Assert.Equal(expected.Description, actual.Description);
			Assert.Equal(expected.IsPublic, actual.IsPublic);
		}
	}
}
