using ComicShelf.Models.Base;
using System;

namespace ComicShelf.Models.Comic
{
	public class CreateComicDto : ICreateEntityDto
	{
		public string Title { get; set; }
		public string Issue { get; set; }
		public string Publisher { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Note { get; set; }
		public byte Image { get; set; }
	}
}
