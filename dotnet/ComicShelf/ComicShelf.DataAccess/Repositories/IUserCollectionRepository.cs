using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IUserCollectionRepository : IGenericRepository<UserCollection>
	{
		IEnumerable<UserCollection> GetUserCollection(int collectionId);
		IEnumerable<UserCollection> GetUserCollectionByComicCollectionId(int comicCollectionId);
	}
}
