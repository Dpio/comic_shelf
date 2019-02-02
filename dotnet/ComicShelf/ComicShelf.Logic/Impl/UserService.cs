using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Models.User;

namespace ComicShelf.Logic.Impl
{
	public class UserService : CrudAppService<User,UserDto, UserDto, UserDto>, IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository repository,IMapper mapper) : base(repository,mapper)
		{
			_userRepository = repository;
		}
	}
}
