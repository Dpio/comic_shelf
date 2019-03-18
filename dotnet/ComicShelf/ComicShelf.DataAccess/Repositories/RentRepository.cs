using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

		public IEnumerable<Rent> GetPendingRequestsForComicByUser(int userId, int comicId)
		{
			var requests = Entities
				.Where(e => e.ReceiverId == userId && e.ComicId == comicId && e.Status.ToString() == "PendingNew" ||
				e.Status.ToString() == "Pending")
				.Include(e => e.Giver)
				.Include(e => e.Receiver)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);
			return requests;
		}

		public Rent GetRentRequestForUserByComic(int userId, int comicId)
		{
			var rentRequest = Entities
				.Where(e => e.GiverId == userId && e.ComicId == comicId)
				.Include(e => e.Giver)
				.Include(e => e.Receiver)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);
			return rentRequest.FirstOrDefault();
		}

		public Rent GetRentRequestForuserInProgress(int userId, int comicId)
		{
			var rentRequest = Entities
				.Where(e => e.ReceiverId == userId && e.ComicId == comicId && e.Status.ToString() == "InProgress")
				.Include(e => e.Giver)
				.Include(e => e.Receiver)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);
			return rentRequest.FirstOrDefault();
		}

		public IEnumerable<Rent> GetInProgressRentsForGiverId(int giverId, int comicId)
		{
			var rents = Entities
				.Where(e => e.GiverId == giverId && e.ComicId == comicId && e.Status.ToString() == "InProgress")
				.Include(e => e.Giver)
				.Include(e => e.Receiver)
				.Include(e => e.Comic)
				.OrderBy(e => e.Id);
			return rents;
		}
	}
}
