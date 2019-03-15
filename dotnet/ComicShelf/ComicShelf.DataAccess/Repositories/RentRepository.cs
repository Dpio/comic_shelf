using System.Collections.Generic;
using System.Linq;
using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicShelf.DataAccess.Repositories
{
	public class RentRepository : GenericRepository<Rent>, IRentRepository
	{
		private readonly ApplicationDbContext _context;

		public RentRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public IEnumerable<Rent> GetRentsForUser(int userId)
		{
			var rents = Entities
				.Where(e => e.GiverId == userId || e.ReceiverId == userId)
				.Include(e => e.Giver)
				.Include(e => e.Receiver)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);
			return rents.ToList();
		}

		public int GetNewRequestsCount(int userId)
		{
			var requestsCount = Entities
				.Where(e => e.GiverId == userId && e.Status.ToString() == "PendingNew")
				.Include(e => e.Giver)
				.Include(e => e.Receiver)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);
			return requestsCount.Count();
		}
	}
}
