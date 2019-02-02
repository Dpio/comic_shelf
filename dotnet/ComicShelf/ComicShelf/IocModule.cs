﻿using ComicShelf.DataAccess.Repositories;
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
		}

		private static void RegisterRepos(IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IComicRepository, ComicRepository>();
		}
	}
}
