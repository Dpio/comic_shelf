using ComicShelf.Models.UserCollection;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IUserCollectionService
	{
		IEnumerable<UserCollectionDto> GetUserCollection(int collectionId);
		UserCollectionDto AddToUserCollection(CreateUserCollectionDto userCollection);
		void DeleteCollectionFromUserCollection(int id);
		UserCollectionDto GetUserCollectionByComicCollectionIdAndCollectionId(int comicCollectionId, int collectionId);
	}
}
