using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.Collection;
using ComicShelf.Models.Comic;
using ComicShelf.Models.ComicCollection;
using ComicShelf.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.Logic.Impl
{
	public class CollectionService : CrudAppService<Collection, CollectionDto, CreateCollectionDto, CollectionDto>, ICollectionService
	{
		private readonly ICollectionRepository _collectionRepository;
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IComicCollectionRepository _comicCollectionRepository;
		private readonly IRentRepository _rentRepository;

		public CollectionService(
			ICollectionRepository repository,
			IMapper mapper,
			IUserRepository userRepository,
			IComicCollectionRepository comicCollectionRepository,
			IRentRepository rentRepository)
			: base(repository, mapper)
		{
			_collectionRepository = repository;
			_mapper = mapper;
			_userRepository = userRepository;
			_comicCollectionRepository = comicCollectionRepository;
			_rentRepository = rentRepository;
		}

		public IEnumerable<CollectionDto> GetAll()
		{
			var collections = _collectionRepository.GetAll();
			var result = Mapper.Map<IEnumerable<CollectionDto>>(collections);
			return result;
		}

		public IEnumerable<CollectionDto> GetCollectionsForUser(int userId)
		{
			var collections = _userRepository.GetCollectionForUser(userId);
			return Mapper.Map<IEnumerable<CollectionDto>>(collections);
		}

		public ComicCollectionDto AddComicToCollection(CreateComicCollectionDto input)
		{
			if (_comicCollectionRepository.GetAll().Any(x => x.CollectionId == input.CollectionId && x.ComicId == input.ComicId))
				throw new AppException(" Already in collection");

			var comicCollection = new ComicCollection()
			{
				CollectionId = input.CollectionId,
				ComicId = input.ComicId
			};
			_comicCollectionRepository.Add(comicCollection);
			_comicCollectionRepository.SaveChanges();
			var comicCollectionDto = _mapper.Map<ComicCollectionDto>(comicCollection);
			return comicCollectionDto;
		}

		public IEnumerable<ComicDto> GetComicsInCollection(int collectionId)
		{
			var comics = _comicCollectionRepository.GetComicsInCollection(collectionId);
			return Mapper.Map<IEnumerable<ComicDto>>(comics);
		}

		public void DeleteComicFromCollection(int id)
		{
			_comicCollectionRepository.Remove(id);
			_comicCollectionRepository.SaveChanges();
		}

		public ComicCollectionDto GetComicCollection(int comicId, int collectionId)
		{
			var comicCollection = _comicCollectionRepository.GetComicCollection(comicId, collectionId);
			var comicCollectionDto = Mapper.Map<ComicCollectionDto>(comicCollection);
			return comicCollectionDto;
		}

		public CollectionDto GetCollectionByName(string name, int userId)
		{
			var collection = _collectionRepository.GetCollectionByName(name, userId);
			var collectionDto = Mapper.Map<CollectionDto>(collection);
			return collectionDto;
		}

		public IEnumerable<CollectionDto> GetWantListForUser(int userId)
		{
			var collections = _userRepository.GetWantListForUser(userId);
			var collectionDtos = Mapper.Map<IEnumerable<CollectionDto>>(collections);
			return collectionDtos;
		}

		public IEnumerable<ComicCollectionDto> GetComicCollectionsByCollectionId(int collectionId)
		{
			var comicCollections = _comicCollectionRepository.GetComicCollectionsByCollectionId(collectionId);
			var comicCollectionDtos = Mapper.Map<IEnumerable<ComicCollectionDto>>(comicCollections);
			return comicCollectionDtos;
		}

		public IEnumerable<UserDto> FindUsersWithComic(int userId, int comicId)
		{
			var comicCollections = _comicCollectionRepository.GetComicCollectionsByComicId(comicId);
			var pendingRequests = _rentRepository.GetPendingRequestsForComicByUser(userId, comicId);
			var rentInProgress = _rentRepository.GetRentRequestForuserInProgress(userId, comicId);
			var requestsAvaible = 4 - pendingRequests.Count();
			var userDtos = new List<UserDto>();

			if (requestsAvaible == 0)
				throw new AppException("You can only make 4 requests for one comic");

			if (rentInProgress != null)
				throw new AppException("You already rented this comic");

			foreach (var comicCollection in comicCollections)
			{
				var user = _userRepository.Get(comicCollection.Collection.UserId);
				var rentRequests = _rentRepository.GetInProgressRentsForGiverId(user.Id, comicId);
				var userDto = Mapper.Map<UserDto>(user);
				if (userId != user.Id && !userDtos.Any(e => e.Id == userDto.Id) && rentRequests.Count() == 0)
				{
					userDtos.Add(userDto);
				}
			}

			foreach (var pendingRequest in pendingRequests)
			{
				if (pendingRequest.ReceiverId == userId)
				{
					var userDtoToDelete = userDtos.Find(e => e.Id == pendingRequest.GiverId);
					if (userDtoToDelete != null)
						userDtos.Remove(userDtoToDelete);
				}
			}
			return userDtos.PickRandom(requestsAvaible);
		}

		public void CheckIfCollectionNameExists(CreateCollectionDto input)
		{
			var collection = _collectionRepository.GetCollectionByName(input.Name, input.UserId);
			if (collection != null)
				throw new AppException("Collection name is already taken");
		}
	}
}
