using AutoFixture;
using ComicShelf.Api;
using ComicShelf.DataAccess;
using ComicShelf.Logic.Impl;
using ComicShelf.Tests.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace ComicShelf.Tests.Controllers
{
	public abstract class BaseTestController
	{
		protected readonly TestServer Server;
		protected readonly HttpClient Client;
		protected readonly ApplicationDbContext Context;
		protected readonly IFixture Fixture;
		protected readonly IConfiguration Configuration;
            
        protected BaseTestController()
		{
            // Arrange
            Fixture = new Fixture().WithStandardCustomization();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            Server = new TestServer(new WebHostBuilder()
				.UseEnvironment("Testing")
				.UseStartup<TestStartup>().UseConfiguration(Configuration));
			Context = Server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            //var authService = Fixture.Freeze<Mock<IAuthenticationService>>();
            Client = Server.CreateClient();
		}
	}
}
