using System;

namespace ComicShelf.DataAccess.Entities
{
	public class Rent : Entity
	{
		public int GiverId { get; set; }
		public int ReceiverId { get; set; }
		public RentStatus Status { get; set; }
		public int ComicId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public virtual User Giver { get; set; }
		public virtual User Receiver { get; set; }
		public virtual Comic Comic { get; set; }
	}
}
