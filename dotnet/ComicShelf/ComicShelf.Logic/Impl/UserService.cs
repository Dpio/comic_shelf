using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository repository, IMapper mapper)
		{
			_userRepository = repository;
		}

		public void Create(User user)
		{
			_userRepository.Add(user);
			_userRepository.SaveChanges();
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
