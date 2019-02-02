using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Comic;

namespace ComicShelf.Logic.Profiles
{
	public class ComicMappingProfile : Profile
	{
		public ComicMappingProfile()
		{
			CreateMap<Comic, ComicDto>();
			CreateMap<ComicDto, Comic>();
		}
	}
}
