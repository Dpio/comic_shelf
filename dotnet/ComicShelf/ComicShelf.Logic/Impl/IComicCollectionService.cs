using ComicShelf.Models.ComicCollection;
using System.Collections.Generic;
using ComicShelf.Models.Collection;

namespace ComicShelf.Logic.Impl
{
	public interface IComicCollectionService
	{
		IEnumerable<CollectionDto> GetComicsCollection(int userId);
		CollectionDto AddToCollection(CreateComicCollectionDto comicCollection);
		void DeleteComicFromCollection(int id);
	}
}
