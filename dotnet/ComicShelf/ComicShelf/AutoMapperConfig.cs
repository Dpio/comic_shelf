using AutoMapper;

namespace ComicShelf.Api
{
	public class AutoMapperConfig
	{
		public static MapperConfiguration Get()
		{
			var mapperConfiguration = new MapperConfiguration(c =>
			{
			});
			return mapperConfiguration;
		}
	}
}
