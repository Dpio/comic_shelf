using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Collection;

namespace ComicShelf.Logic.Profiles
{
	public class CollectionMappingProfile : Profile
	{
		public CollectionMappingProfile()
		{
			CreateMap<Collection, CollectionDto>();
			CreateMap<CollectionDto, Collection>()
			.ForMember(x => x.ComicsCollection, e => e.Ignore());
			CreateMap<CreateCollectionDto, Collection>()
				.ForMember(x => x.ComicsCollection, e => e.Ignore())
				.ForMember(x => x.Id, e => e.Ignore());
		}
	}
}
