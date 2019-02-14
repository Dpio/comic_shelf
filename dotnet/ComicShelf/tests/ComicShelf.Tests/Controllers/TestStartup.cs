using ComicShelf.Api;
using ComicShelf.DataAccess;
using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.Tests.Controllers
{
	public class TestStartup : Startup
	{
		public TestStartup(IConfiguration configuration, IHostingEnvironment env) : base(configuration,env)
		{
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IConfiguration>(Configuration);
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseInMemoryDatabase(databaseName: "testDb"));
			services.AddMvc();
			IocModule.RegisterDependencies(services);
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "Test Scheme";
				options.DefaultChallengeScheme = "Test Scheme";
			}).AddTestAuth(o => { });

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "API"
				});
			});
		}

	}
}
