using ComicShelf.Api;
using ComicShelf.DataAccess;
using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicShelf.Api
{
	public class Startup
	{
		private readonly IHostingEnvironment _env;

		public Startup(IConfiguration configuration, IHostingEnvironment env)
		{
			_env = env;
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton<IConfiguration>(Configuration);
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

			string secret = Configuration.GetSection("AppSettings:Secret").Value;
			var key = Encoding.ASCII.GetBytes(secret);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.Events = new JwtBearerEvents
				{
					OnTokenValidated = context =>
					{
						var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
						var userId = int.Parse(context.Principal.Identity.Name);
						var user = userService.GetById(userId);
						if (user == null)
						{
							// return unauthorized if user no longer exists
							context.Fail("Unauthorized");
						}
						return Task.CompletedTask;
					}
				};
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "API"
				});
				c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
				c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
							   { "Bearer", Enumerable.Empty<string>() },
						   });
				c.OperationFilter<FileUploadOperation>();
			});

			if (_env.IsEnvironment("Testing"))
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseInMemoryDatabase(databaseName: "testDb"));
			}
			else
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("Default")));
			}
			services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
			{
				builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithOrigins("http://localhost:4200");
			}));

			services.AddMvc();
			services.AddSignalR();
			services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
			.AllowAnyMethod().AllowAnyHeader()));
			services.AddAuthorization();
			IocModule.RegisterDependencies(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				
			}
			//app.UseHttpsRedirection();
			//app.UseStaticFiles();
			//app.UseSpaStaticFiles();

			//app.UseMvc(routes =>
			//{
			//	routes.MapRoute(
			//		name: "default",
			//		template: "{controller}/{action=Index}/{id?}");
			//});

			//app.UseSpa(spa =>
			//{
			//	// To learn more about options for serving an Angular SPA from ASP.NET Core,
			//	// see https://go.microsoft.com/fwlink/?linkid=864501

			//	spa.Options.SourcePath = "ClientApp";

			//	if (env.IsDevelopment())
			//	{
			//		spa.UseAngularCliServer(npmScript: "start");
			//	}
			//});

			app.UseCors("AllowAll");
			app.UseSwagger();
			app.UseSignalR(routes =>
			{
				routes.MapHub<NotifyHub>("/notify");
			});
			app.UseSwaggerUI(c =>
			{

				c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
			});
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
