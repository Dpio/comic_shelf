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
				.Include(e => e.Collection)
				.Include(e => e.ComicCollection)
				.Where(e => e.CollectionId == collectionId)
				.OrderBy(e => e.Id);
			return usercollection.ToList();
		}

		public IEnumerable<UserCollection> GetUserCollectionByComicCollectionId(int comicCollectionId)
		{
			var usercollection = Entities
				.Include(e => e.Collection)
				.Include(e => e.ComicCollection)
				.Where(e => e.ComicCollectionId == comicCollectionId)
				.OrderBy(e => e.Id);

			var test = usercollection.SelectMany(uc => uc.ComicCollection).ToList();

			return usercollection.ToList();
		}

		public IEnumerable<UserCollection> GetU(int collectionId)
		{
			var usercollection = Entities
				.Include(e => e.Collection)
				.Include(e => e.ComicCollection)
				.Where(e => e.CollectionId == collectionId)
				.OrderBy(e => e.Id);
			List<UserCollection> userCollections = usercollection.ToList();

			var users = userCollections.SelectMany(uc => uc.ComicCollection).Select(cc => cc.User).Distinct(); 

			return userCollections;
		}
	}
}
