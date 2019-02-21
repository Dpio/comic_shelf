using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class ComicCollection : Entity
    {
		public int ComicId { get; set; }
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public virtual Comic Comic { get; set; }
        public virtual ICollection<UserCollection> UserCollection { get; set; }
    }
}
