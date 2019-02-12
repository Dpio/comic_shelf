using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.UserCollection;

namespace ComicShelf.Logic.Profiles
{
	public class UserCollectionMappingProfile : Profile
	{
		public UserCollectionMappingProfile()
		{
			CreateMap<UserCollection, UserCollectionDto>();
			CreateMap<UserCollectionDto, UserCollection>()
				.ForMember(x => x.Collections, e => e.Ignore())
				.ForMember(x => x.ComicCollections, e => e.Ignore());
			CreateMap<CreateUserCollectionDto, UserCollection>()
				.ForMember(x => x.Id, e => e.Ignore())
				.ForMember(x => x.Collections, e => e.Ignore())
				.ForMember(x => x.ComicCollections, e => e.Ignore());
		}
	}
}
