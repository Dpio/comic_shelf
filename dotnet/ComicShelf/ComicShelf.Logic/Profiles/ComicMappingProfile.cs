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
			CreateMap<CreateComicDto, Comic>()
				.ForMember(x => x.Id, e => e.Ignore());
		}
	}
}
