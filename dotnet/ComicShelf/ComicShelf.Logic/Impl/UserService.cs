using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.Logic.Impl
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ICollectionRepository _collectionRepository;

		public UserService(IUserRepository repository, ICollectionRepository collectionRepository, IMapper mapper)
		{
			_userRepository = repository;
			_collectionRepository = collectionRepository;
		}

		public User Create(User user)
		{
			var users = _userRepository.GetAll();
			if (users.Any(x => x.Name == user.Name && x.GoogleId == user.GoogleId))
				throw new AppException("User already exists");

			_userRepository.Add(user);
			_userRepository.SaveChanges();

			var collection = new Collection();
			collection.Name = "My collection";
			collection.Description = "This is my collection";
			collection.IsPublic = true;
			collection.IsWantList = false;
			collection.UserId = user.Id;

			_collectionRepository.Add(collection);
			_collectionRepository.SaveChanges();

			return user;
		}

		public IEnumerable<User> GetAll()
		{
			return _userRepository.GetAll();
		}

		public User GetById(int id)
		{
			return _userRepository.Get(id);
		}

		public void Update(User input)
		{
			var user = _userRepository.Get(input.Id);

			if (user == null)
				throw new AppException("User not found");

			user.Name = input.Name;
			user.GivenName = input.GivenName;
			user.GoogleId = input.GoogleId;
			user.Picture = input.Picture;

			_userRepository.Update(user);
			_userRepository.SaveChanges();
		}

		public void Delete(int id)
		{
			var user = _userRepository.Get(id);
			if (user != null)
			{
				_userRepository.Remove(id);
				_userRepository.SaveChanges();
			}
		}
	}
}
