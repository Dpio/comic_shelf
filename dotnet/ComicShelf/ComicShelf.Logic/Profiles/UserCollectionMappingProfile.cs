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
				.ForMember(x => x.Collection, e => e.Ignore())
				.ForMember(x => x.ComicCollection, e => e.Ignore());
			CreateMap<CreateUserCollectionDto, UserCollection>()
				.ForMember(x => x.Id, e => e.Ignore())
				.ForMember(x => x.Collection, e => e.Ignore())
				.ForMember(x => x.ComicCollection, e => e.Ignore());
		}
	}
}
