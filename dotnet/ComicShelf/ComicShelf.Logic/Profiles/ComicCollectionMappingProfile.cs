using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.ComicCollection;

namespace ComicShelf.Logic.Profiles
{
	public class ComicCollectionMappingProfile : Profile
	{
		public ComicCollectionMappingProfile()
		{
			CreateMap<ComicCollection, ComicCollectionDto>();
			CreateMap<ComicCollectionDto, ComicCollection>();
			CreateMap<CreateComicCollectionDto, ComicCollection>()
				.ForMember(x => x.Id, e => e.Ignore());
		}
	}
}
