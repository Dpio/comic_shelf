using ComicShelf.Models.Base;

namespace ComicShelf.Models.ComicCollection
{
	public class CreateComicCollectionDto : ICreateEntityDto
	{
		public int ComicId { get; set; }
		public int UserId { get; set; }
	}
}
