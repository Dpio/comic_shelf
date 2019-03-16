using ComicShelf.Logic.Base;
using ComicShelf.Models.Rent;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IRentService : ICrudAppService<RentDto, CreateRentDto, UpdateRentDto>
	{
		IEnumerable<RentDto> GetRentsForUser(int userId);
		int GetNewRequestsCount(int userId);
	}
}
