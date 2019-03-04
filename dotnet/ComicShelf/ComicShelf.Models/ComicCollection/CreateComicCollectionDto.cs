using ComicShelf.Models.Base;

namespace ComicShelf.Models.ComicCollection
{
	public class CreateComicCollectionDto : ICreateEntityDto
	{
		public int CollectionId { get; set; }
		public int ComicId { get; set; }
	}
}
