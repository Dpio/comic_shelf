namespace ComicShelf.DataAccess.Entities
{
	public class Collection : Entity
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
	}
}
