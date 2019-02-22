using ComicShelf.DataAccess.Entities;

namespace ComicShelf.DataAccess.Repositories
{
	public interface ICollectionRepository : IGenericRepository<Collection>
	{
		Collection getByName(string name);
	}
}
