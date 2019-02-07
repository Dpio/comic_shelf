using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.DataAccess.Repositories
{
	public class UserCollectionRepository : GenericRepository<UserCollection>, IUserCollectionRepository
	{
		private readonly ApplicationDbContext _context;

		public UserCollectionRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public IEnumerable<UserCollection> GetUserCollection(int collectionId)
		{
			var userCollections = _context.UserCollections.Include(uc => uc.ComicCollections).Include(uc => uc.Collections).Where(uc => uc.CollectionId == collectionId);
			return userCollections;
		}
	}
}
