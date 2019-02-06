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
			CreateMap<CollectionDto, Collection>();
			CreateMap<CreateCollectionDto, Collection>();
		}
	}
}
