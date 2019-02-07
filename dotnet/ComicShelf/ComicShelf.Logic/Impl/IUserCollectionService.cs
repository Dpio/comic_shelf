using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.UserCollection;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IUserCollectionService
	{
		IEnumerable<UserCollection> GetUserCollection(int collectionId);
		UserCollection AddToUserCollection(UserCollectionDto userCollection);
		void DeleteCollectionFromUserCollection(int id);
	}
}
