using AutoMapper;
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
			});
			return mapperConfiguration;
		}
	}
}
