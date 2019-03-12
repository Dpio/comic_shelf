using ComicShelf.Logic.Base;
using ComicShelf.Models.Rent;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IRentService : ICrudAppService<RentDto, CreateRentDto, RentDto>
	{
		IEnumerable<RentDto> GetRentsForUser(int userId);
	}
}
