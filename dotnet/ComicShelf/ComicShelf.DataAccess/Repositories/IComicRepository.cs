using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IComicRepository : IGenericRepository<Comic>
	{
		IEnumerable<Comic> GetAllComics();
		Task<Comic> GetWithDetails(int id);
		Comic Update(Comic comic);
	}
}
