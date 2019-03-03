using System.Collections;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class Collection : Entity
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
        public virtual ICollection<Comic> Comics { get; set; }
        public bool IsWantList { get; set; }
    }
}
