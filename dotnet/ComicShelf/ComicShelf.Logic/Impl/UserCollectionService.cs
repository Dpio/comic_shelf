using AutoMapper;
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
		private IMapper _mapper;

		public UserCollectionService(IUserCollectionRepository userCollectionRepository, IMapper mapper)
		{
			_userCollectionRepository = userCollectionRepository;
			_mapper = mapper;
		}

		public UserCollectionDto AddToUserCollection(CreateUserCollectionDto input)
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
			var userCollectionDto = _mapper.Map<UserCollectionDto>(userCollection);
			return userCollectionDto;
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
