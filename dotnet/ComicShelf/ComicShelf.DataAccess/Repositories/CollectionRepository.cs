using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
	{
		private readonly ApplicationDbContext _context;

		public CollectionRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
