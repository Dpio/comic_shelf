using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IComicCollectionRepository : IGenericRepository<ComicCollection>
	{
		IEnumerable<ComicCollection> GetComicsCollection(int userId);
		ComicCollection GetWithDetails(int id);
		ComicCollection getComicCollection(int userId, int comicId);
	}
}
