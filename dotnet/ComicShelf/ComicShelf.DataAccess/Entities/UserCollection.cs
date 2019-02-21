namespace ComicShelf.DataAccess.Entities
{
    public class UserCollection : Entity
	{
        public int CollectionId { get; set; }
		public int ComicCollectionId { get; set; }
		public virtual Collection Collection { get; set; }
		public virtual ComicCollection ComicCollection { get; set; }
	}
}
