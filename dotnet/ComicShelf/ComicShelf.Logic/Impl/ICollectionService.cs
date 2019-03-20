using ComicShelf.Logic.Base;
using ComicShelf.Models.Collection;
using ComicShelf.Models.Comic;
using ComicShelf.Models.ComicCollection;
using ComicShelf.Models.User;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface ICollectionService : ICrudAppService<CollectionDto, CreateCollectionDto, CollectionDto>
	{
		IEnumerable<CollectionDto> GetAll();
		IEnumerable<CollectionDto> GetCollectionsForUser(int userId);
		ComicCollectionDto AddComicToCollection(CreateComicCollectionDto input);
		IEnumerable<ComicDto> GetComicsInCollection(int collectionId);
		void DeleteComicFromCollection(int id);
		ComicCollectionDto GetComicCollection(int comicId, int collectionId);
		CollectionDto GetCollectionByName(string name, int userId);
		IEnumerable<CollectionDto> GetWantListForUser(int userId);
		IEnumerable<ComicCollectionDto> GetComicCollectionsByCollectionId(int collectionId);
		IEnumerable<UserDto> FindUsersWithComic(int userId, int comicId);
		void CheckIfCollectionNameExists(CreateCollectionDto input);
	}
}
