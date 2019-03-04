using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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

		public Collection GetCollectionByName(string name, int userId)
		{
			var collection = Entities
				.Where(e => e.Name == name && e.UserId == userId)
				.Include(e => e.ComicsCollection);
			return collection.First();
		}
	}
}
