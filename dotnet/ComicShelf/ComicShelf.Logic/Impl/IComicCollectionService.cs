using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IComicCollectionService
	{
		IEnumerable<ComicCollection> GetComicsCollection(int userId);
		ComicCollection AddToCollection(int userId, int comicId);
		void DeleteComicFromCollection(int id);
	}
}
