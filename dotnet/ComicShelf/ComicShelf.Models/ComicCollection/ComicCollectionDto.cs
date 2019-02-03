using ComicShelf.Models.Base;

namespace ComicShelf.Models.ComicCollection
{
	public class ComicCollectionDto : EntityDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int ComicId { get; set; }
		public int UserId { get; set; }
	}
}
