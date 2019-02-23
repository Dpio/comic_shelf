using ComicShelf.Models.ComicCollection;
using ComicShelf.Models.User;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IComicCollectionService
	{
		IEnumerable<ComicCollectionDto> GetComicsCollection(int userId);
		ComicCollectionDto AddToCollection(CreateComicCollectionDto comicCollection);
		void DeleteComicFromCollection(int id);
		ComicCollectionDto GetComicCollection(int userId, int comicId);
		IEnumerable<UserDto> GetUsersWithComic(int comicId);
	}
}
