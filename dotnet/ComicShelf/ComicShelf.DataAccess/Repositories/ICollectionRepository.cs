using System.Collections.Generic;
using ComicShelf.DataAccess.Entities;

namespace ComicShelf.DataAccess.Repositories
{
	public interface ICollectionRepository : IGenericRepository<Collection>
	{
		Collection GetByName(string name);
	    IEnumerable<Collection> GetByUserId(int userId);
	}
}
