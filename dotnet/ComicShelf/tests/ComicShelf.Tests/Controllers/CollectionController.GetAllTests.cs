using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Collection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class CollectionController_GetAllTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetAllCollections()
		{
			string Name = String.Empty;
			var collections = Fixture.Build<Collection>().CreateMany(5).OrderBy(p => p.Id).ToList();
			var collection = Fixture.Build<Collection>().With(t => t.Name, Name).Create();
			Context.Collections.Add(collection);
			Context.Collections.AddRange(collections);
			Context.SaveChanges();

			// Act
			var response = await Client.GetAsync("api/Collection/getAll");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<IEnumerable<CollectionDto>>(jsonResult).ToList();
			Assert.Contains(actual, dto => dto.Name== collection.Name);
			Assert.Equal(6, actual: actual.Count);
		}
	}
}
