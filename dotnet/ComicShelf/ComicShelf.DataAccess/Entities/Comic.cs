using System;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class Comic : Entity
	{
		public string Title { get; set; }
		public int Issue { get; set; }
		public string Volume { get; set; }
		public string Publisher { get; set; }
		public string Series { get; set; }
		public string ScriptWriter { get; set; }
		public string Draftsman { get; set; }
		public string Translator { get; set; }
		public string Cover { get; set; }
		public DateTime PremierDate { get; set; }
		public string Note { get; set; }
		public byte[] Image { get; set; }
		public virtual ICollection<ComicCollection> ComicsCollections { get; set; }
	}
}
