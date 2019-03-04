using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicShelf.DataAccess.Repositories
{
	public class ComicRepository : GenericRepository<Comic>, IComicRepository
	{
		private readonly ApplicationDbContext _context;

		public ComicRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public override Comic Get(int id)
		{
			var comic = Entities
					.FirstOrDefault(e => e.Id == id);
			return comic;
		}

		public override async Task<Comic> GetAsync(int id)
		{
			var comic = await Entities
					.FirstOrDefaultAsync(e => e.Id == id);
			return comic;
		}

		public IEnumerable<Comic> GetAllComics()
		{
			var comics = Entities
				.OrderBy(e => e.Id);
			return comics.ToList();
		}

		public async Task<Comic> GetWithDetails(int id)
		{
			var comic = await Entities
				.FirstOrDefaultAsync(e => e.Id == id);
			return comic;
		}

		public Comic Update(Comic comic)
		{
			var result = Entities.Update(comic);
			return result.Entity;
		}
	}
}
