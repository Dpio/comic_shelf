using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IUserService
	{
		IEnumerable<User> GetAll();
		User GetById(int id);
		void Create(User user);
		void Update(User user);
		void Delete(int id);
	}
}
