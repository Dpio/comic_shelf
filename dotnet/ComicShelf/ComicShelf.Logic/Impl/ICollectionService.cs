using ComicShelf.Logic.Base;
using ComicShelf.Models.Collection;
using ComicShelf.Models.Comic;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface ICollectionService : ICrudAppService<CollectionDto, CreateCollectionDto, CollectionDto>
	{
		IEnumerable<CollectionDto> GetAll();
		IEnumerable<string> GetUserCollection(int userId);
		CollectionDto GetByName(string name);
		IEnumerable<ComicDto> GetComicsForUser(int collectionId, int userId);
	}
}
