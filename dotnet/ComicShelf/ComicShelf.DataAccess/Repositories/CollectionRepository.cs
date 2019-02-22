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

		public Collection getByName(string name)
		{
			var collection = Entities
				.Where(c => c.Name == name);
			return collection.FirstOrDefault();
		}
	}
}
