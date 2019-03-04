using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.User;

namespace ComicShelf.Logic.Profiles
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>()
				.ForMember(x => x.Collections, e => e.Ignore());
			CreateMap<CreateUserDto, User>()
				.ForMember(x => x.Id, e => e.Ignore())
				.ForMember(x => x.Collections, e => e.Ignore());
		}
	}
}
