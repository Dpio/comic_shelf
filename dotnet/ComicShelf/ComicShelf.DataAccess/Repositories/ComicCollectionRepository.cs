using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.DataAccess.Repositories
{
	public class ComicCollectionRepository : GenericRepository<ComicCollection>, IComicCollectionRepository
	{
		private readonly ApplicationDbContext _context;

		public ComicCollectionRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public IEnumerable<ComicCollection> GetComicsCollection(int userId)
		{
			var comicCollections = Entities
				.Include(e => e.Comic)
				.Include(e => e.User)
				.Where(e => e.UserId == userId)
				.OrderBy(e => e.Id);
			return comicCollections.ToList();
		}
	}
}
