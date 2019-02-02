using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicShelf.DataAccess.Repositories
{
	public class ComicCollectionRepository : GenericRepository<ComicCollection>, IComicCollectionRepository
	{
		private readonly ApplicationDbContext _context;

		public ComicCollectionRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
