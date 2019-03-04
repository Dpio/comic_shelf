using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.DataAccess.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(User user)
		{
			_context.Update(user);
		}

		public IEnumerable<Collection> GetCollectionForUser(int userId)
		{
			var user = Entities.
				Include(e => e.Collections)
				.Where(e => e.Id == userId);
			return user.SelectMany(e => e.Collections).Where(e => e.IsWantList == false);
		}

		public IEnumerable<Collection> GetWantListForUser(int userId)
		{
			var user = Entities.
				Include(e => e.Collections)
				.Where(e => e.Id == userId);
			return user.SelectMany(e => e.Collections).Where(e => e.IsWantList == true);
		}
	}
}
