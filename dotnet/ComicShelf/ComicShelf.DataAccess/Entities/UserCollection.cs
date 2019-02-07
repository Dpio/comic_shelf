using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class UserCollection : Entity
	{
		public int CollectionId { get; set; }
		public int ComicCollectionId { get; set; }
		public virtual ICollection<Collection> Collections { get; set; }
		public virtual ICollection<ComicCollection> ComicCollections { get; set; }
	}
}
