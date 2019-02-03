using ComicShelf.Models.Base;

namespace ComicShelf.Models.ComicCollection
{
	public class ComicCollectionDto : EntityDto
	{
		public int ComicId { get; set; }
		public int UserId { get; set; }
	}
}
