using ComicShelf.Models.Base;
using ComicShelf.Models.Comic;
using ComicShelf.Models.User;
using System;

namespace ComicShelf.Models.Rent
{
	public class RentDto : EntityDto
	{
		public int GiverId { get; set; }
		public int ReceiverId { get; set; }
		public RentStatus Status { get; set; }
		public int ComicId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public ComicDto Comic { get; set; }
		public UserDto Giver { get; set; }
		public UserDto Receiver { get; set; }
	}
}
