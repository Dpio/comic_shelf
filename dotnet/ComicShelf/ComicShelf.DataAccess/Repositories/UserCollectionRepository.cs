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
			var usercollection = Entities
				.Include(e => e.Collections)
				.Include(e => e.ComicCollections)
				.Where(e => e.CollectionId == collectionId)
				.OrderBy(e => e.Id);
			return usercollection.ToList();
		}
	}
}
