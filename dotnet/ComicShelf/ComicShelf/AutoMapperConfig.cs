﻿using AutoMapper;
using ComicShelf.Logic.Profiles;

namespace ComicShelf.Api
{
	public class AutoMapperConfig
	{
		public static MapperConfiguration Get()
		{
			var mapperConfiguration = new MapperConfiguration(c =>
			{
				c.AddProfile(new UserMappingProfile());
				c.AddProfile(new ComicMappingProfile());
				c.AddProfile(new CollectionMappingProfile());
				c.AddProfile(new ComicCollectionMappingProfile());
				c.AddProfile(new RentMappingProfile());
			});
			return mapperConfiguration;
		}
	}
}
