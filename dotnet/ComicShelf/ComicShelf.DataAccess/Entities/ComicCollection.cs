namespace ComicShelf.DataAccess.Entities
{
	public class ComicCollection : Entity
	{
		public int CollectionId { get; set; }
		public int ComicId { get; set; }
		public virtual Collection Collection { get; set; }
		public virtual Comic Comic { get; set; }
	}
}
