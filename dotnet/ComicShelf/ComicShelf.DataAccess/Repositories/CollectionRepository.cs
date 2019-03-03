using System.Collections.Generic;
using ComicShelf.DataAccess.Entities;
using System.Linq;

namespace ComicShelf.DataAccess.Repositories
{
	public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
	{
		private readonly ApplicationDbContext _context;

		public CollectionRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public Collection GetByName(string name)
		{
			var collection = Entities
				.Where(c => c.Name == name);
			return collection.FirstOrDefault();
		}

	    public IEnumerable<Collection> GetByUserId(int userId)
	    {
	        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
	        return user.ComicsCollections.ToList();
	    }
	}
}
