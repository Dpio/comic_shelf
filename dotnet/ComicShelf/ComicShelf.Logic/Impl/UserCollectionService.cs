using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.UserCollection;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.Logic.Impl
{
	public class UserCollectionService : IUserCollectionService
	{
		private readonly IUserCollectionRepository _userCollectionRepository;

		public UserCollectionService(IUserCollectionRepository UserCollectionRepository)
		{
			_userCollectionRepository = UserCollectionRepository;
		}

		public UserCollection AddToUserCollection(UserCollectionDto input)
		{
			if (_userCollectionRepository.GetAll().Any(x => x.CollectionId == input.CollectionId && x.ComicCollectionId == input.ComicCollectionId))
				throw new AppException(" Already in collection");
			var userCollection = new UserCollection()
			{
				CollectionId = input.CollectionId,
				ComicCollectionId = input.ComicCollectionId
			};
			_userCollectionRepository.Add(userCollection);
			_userCollectionRepository.SaveChanges();
			return userCollection;
		}

		public IEnumerable<UserCollection> GetUserCollection(int collectionId)
		{
			return _userCollectionRepository.GetUserCollection(collectionId);
		}

		public void DeleteCollectionFromUserCollection(int id)
		{
			var collection = _userCollectionRepository.Get(id);
			if (collection != null)
			{
				_userCollectionRepository.Remove(id);
				_userCollectionRepository.SaveChanges();
			}
		}
	}
}
