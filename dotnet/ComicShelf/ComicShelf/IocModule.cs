using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace ComicShelf.Api
{
	public class IocModule
	{
		public static void RegisterDependencies(IServiceCollection services)
		{
			InitAutomapper(services);
			RegisterServices(services);
			RegisterRepos(services);
		}

		private static void InitAutomapper(IServiceCollection services)
		{
			var mapperConfiguration = AutoMapperConfig.Get();
			services.AddSingleton(x => mapperConfiguration.CreateMapper());
		}

		private static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IComicService, ComicService>();
			services.AddScoped<IComicCollectionService, ComicCollectionService>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ICollectionService, CollectionService>();
			services.AddScoped<IUserCollectionService, UserCollectionService>();
		}

		private static void RegisterRepos(IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IComicRepository, ComicRepository>();
			services.AddScoped<IComicCollectionRepository, ComicCollectionRepository>();
			services.AddScoped<ICollectionRepository, CollectionRepository>();
			services.AddScoped<IUserCollectionRepository, UserCollectionRepository>();
		}
	}
}
