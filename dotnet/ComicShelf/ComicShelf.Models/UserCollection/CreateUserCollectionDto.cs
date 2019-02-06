using ComicShelf.Models.Base;

namespace ComicShelf.Models.UserCollection
{
	public class CreateUserCollectionDto : ICreateEntityDto
	{
		public int CollectionId { get; set; }
		public int ComicCollectionId { get; set; }
	}
}
