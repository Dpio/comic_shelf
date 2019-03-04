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

		public IEnumerable<Comic> GetComicsInCollection(int collectionId)
		{
			var comicCollections = Entities
				.Where(e => e.CollectionId == collectionId)
				.Include(e => e.Collection)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);

			var comics = comicCollections.Select(e => e.Comic);
			return comics;
		}

		public ComicCollection GetComicCollection(int comicId, int collectionId)
		{
				var comicCollection = Entities
				.Where(e => e.CollectionId == collectionId && e.ComicId == comicId)
				.Include(e => e.Collection)
				.Include(e => e.Comic);
			return comicCollection.First();
		}

		public IEnumerable<ComicCollection> GetComicCollectionsByCollectionId(int collectionId)
		{
			var comicCollections = Entities
				.Where(e => e.CollectionId == collectionId)
				.Include(e => e.Collection)
				.Include(e => e.Comic);
			return comicCollections.ToList();
		}
	}
}