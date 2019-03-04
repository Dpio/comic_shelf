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
			CreateMap<ComicCollectionDto, ComicCollection>()
			.ForMember(x => x.Collection, e => e.Ignore())
			.ForMember(x => x.Comic, e => e.Ignore());
			CreateMap<CreateComicCollectionDto, ComicCollection>()
				.ForMember(x => x.Collection, e => e.Ignore())
				.ForMember(x => x.Comic, e => e.Ignore())
				.ForMember(x => x.Id, e => e.Ignore());
		}
	}
}
