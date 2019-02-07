using ComicShelf.DataAccess.Entities;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		void Update(User user);
	}
}
