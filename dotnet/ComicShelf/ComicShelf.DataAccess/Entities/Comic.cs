using System;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class Comic : Entity
	{
		public string Title { get; set; }
		public string Issue { get; set; }
		public string Publisher { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Note { get; set; }
		public byte Image { get; set; }
		public virtual ICollection<ComicCollection> ComicsCollection { get; set; }
	}
}
