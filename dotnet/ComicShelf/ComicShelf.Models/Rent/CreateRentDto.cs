using ComicShelf.Models.Base;
using System;

namespace ComicShelf.Models.Rent
{
	public class CreateRentDto : ICreateEntityDto
	{
		public int GiverId { get; set; }
		public int ReceiverId { get; set; }
		public RentStatus Status { get; set; }
		public int ComicId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
