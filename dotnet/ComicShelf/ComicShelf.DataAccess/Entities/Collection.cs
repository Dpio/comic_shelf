using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class Collection : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
		public bool IsWantList { get; set; }
		public int UserId { get; set; }
		public virtual ICollection<ComicCollection> ComicsCollection { get; set; }
	}
}
