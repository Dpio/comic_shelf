using ComicShelf.DataAccess.Entities;

namespace ComicShelf.DataAccess.Repositories
{
	public interface ICollectionRepository : IGenericRepository<Collection>
	{
		Collection GetCollectionByName(string name, int userId);
	}
}
