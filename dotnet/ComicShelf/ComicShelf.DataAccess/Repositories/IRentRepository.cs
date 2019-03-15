using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IRentRepository : IGenericRepository<Rent>
	{
		IEnumerable<Rent> GetRentsForUser(int userId);
		int GetNewRequestsCount(int userId);
	}
}
