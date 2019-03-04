using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IComicCollectionRepository : IGenericRepository<ComicCollection>
	{
		IEnumerable<Comic> GetComicsInCollection(int collectionId);
		ComicCollection GetComicCollection(int comicId, int collectionId);
	}
}
