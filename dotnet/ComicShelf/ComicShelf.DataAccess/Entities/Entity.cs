using System;

namespace ComicShelf.DataAccess.Entities
{
	[Serializable]
	public abstract class Entity : IEntity
	{
		public int Id { get; set; }
	}
}
