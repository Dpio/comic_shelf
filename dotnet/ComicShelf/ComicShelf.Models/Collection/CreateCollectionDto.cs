using ComicShelf.Models.Base;

namespace ComicShelf.Models.Collection
{
	public class CreateCollectionDto : ICreateEntityDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
	}
}
