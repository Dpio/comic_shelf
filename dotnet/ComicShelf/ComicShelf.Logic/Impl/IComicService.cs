using ComicShelf.Logic.Base;
using ComicShelf.Models.Comic;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IComicService : ICrudAppService<ComicDto, CreateComicDto, ComicDto>
	{
		IEnumerable<ComicDto> GetAll();
		ComicDto Get(int id);
	}
}
