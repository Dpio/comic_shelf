using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.Rent;

namespace ComicShelf.Logic.Impl
{
	public class RentService : CrudAppService<Rent, RentDto, CreateRentDto, UpdateRentDto>, IRentService
	{
		private readonly IRentRepository _rentRepository;

		public RentService(IRentRepository repository, IMapper mapper) : base(repository, mapper)
		{
			_rentRepository = repository;
		}

		public IEnumerable<RentDto> GetRentsForUser(int userId)
		{
			var rents = _rentRepository.GetRentsForUser(userId);
			var rentDtos = Mapper.Map<IEnumerable<RentDto>>(rents);
			return rentDtos;
		}

		public int GetNewRequestsCount(int userId)
		{
			var requestsCount = _rentRepository.GetNewRequestsCount(userId);
			return requestsCount;
		}

		public override RentDto Create(CreateRentDto input)
		{
			var pendingRequestsCount = _rentRepository.GetPendingRequestsCountForComicByUser(input.ReceiverId, input.ComicId);
			var avaibleRequests = 4 - pendingRequestsCount;
			if (avaibleRequests == 0)
				throw new AppException("You can only make 4 requests for a comic");

			return base.Create(input);
		}

	}
}
