using ComicShelf.Models.Base;

namespace ComicShelf.Models.UserCollection
{
	public class UserCollectionDto : EntityDto
	{
		public int CollectionId { get; set; }
		public int ComicCollectionId { get; set; }
	}
}
