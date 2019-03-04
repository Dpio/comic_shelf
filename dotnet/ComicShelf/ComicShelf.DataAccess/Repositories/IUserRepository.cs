using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		void Update(User user);
		IEnumerable<Collection> GetCollectionForUser(int userId);
		IEnumerable<Collection> GetWantListForUser(int userId);
	}
}
