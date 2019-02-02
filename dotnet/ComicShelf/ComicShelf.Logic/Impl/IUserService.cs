using ComicShelf.Logic.Base;
using ComicShelf.Models.User;

namespace ComicShelf.Logic.Impl
{
	public interface IUserService : ICrudAppService<UserDto, UserDto, UserDto>
	{
	}
}
