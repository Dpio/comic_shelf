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
			var comicCollections = _context.ComicCollections.Include(cc => cc.Comic).Include(cc => cc.User).Where(cc => cc.UserId == userId);
			return comicCollections;
		}
	}
}
