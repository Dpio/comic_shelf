using System.Collections.Generic;

namespace ComicShelf.DataAccess.Entities
{
	public class User : Entity
	{
		public string GoogleId { get; set; }
		public string Name { get; set; }
		public string GivenName { get; set; }
		public string Email { get; set; }
		public string Picture { get; set; }
		public virtual ICollection<Collection> ComicsCollections { get; set; }
	}
}
