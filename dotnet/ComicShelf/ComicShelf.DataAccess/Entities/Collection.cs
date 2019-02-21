using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class Collection : Entity
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
        public virtual ICollection<UserCollection> UserCollection { get; set; }
	}
}
