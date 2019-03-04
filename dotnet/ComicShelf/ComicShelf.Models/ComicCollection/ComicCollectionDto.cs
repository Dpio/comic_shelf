using ComicShelf.Models.Base;

namespace ComicShelf.Models.ComicCollection
{
	public class ComicCollectionDto : EntityDto
	{
		public int CollectionId { get; set; }
		public int ComicId { get; set; }
	}
}
