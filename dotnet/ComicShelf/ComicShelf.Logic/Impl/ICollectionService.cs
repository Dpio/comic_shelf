using ComicShelf.Logic.Base;
using ComicShelf.Models.Collection;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface ICollectionService : ICrudAppService<CollectionDto, CreateCollectionDto, CollectionDto>
	{
		IEnumerable<CollectionDto> GetAll();
	}
}
