using AutoFixture;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ComicShelf.Tests.Controllers
{
	public class UserController_GetAllTests : BaseTestController
	{
		[Fact]
		public async Task ShouldGetAllUsers()
		{
			string Name = String.Empty;
			var users = Fixture.Build<User>().CreateMany(5).OrderBy(p => p.Id).ToList();
			var user = Fixture.Build<User>().With(t => t.Name, Name).Create();
			Context.Users.Add(user);
			Context.Users.AddRange(users);
			Context.SaveChanges();

			// Act
			var response = await Client.GetAsync("api/User/getAll");
			response.EnsureSuccessStatusCode();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			string jsonResult = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult).ToList();
			Assert.Contains(actual, dto => dto.Name == user.Name);
			Assert.Equal(6, actual: actual.Count);
		}
	}
}
