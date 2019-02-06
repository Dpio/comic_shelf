using ComicShelf.Models.Base;

namespace ComicShelf.Models.Collection
{
	public class CollectionDto : EntityDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
	}
}
