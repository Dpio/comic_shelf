using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Models.Collection;
using ComicShelf.Models.Comic;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.Logic.Impl
{
	public class CollectionService : CrudAppService<Collection, CollectionDto, CreateCollectionDto, CollectionDto>, ICollectionService
	{
		private readonly ICollectionRepository _collectionRepository;
		private readonly IComicCollectionRepository _comicCollectionRepository;
		private readonly IUserCollectionRepository _userCollectionRepository;
		private readonly IMapper _mapper;

		public CollectionService(
			ICollectionRepository repository,
			IComicCollectionRepository comicCollectionRepository,
			IUserCollectionRepository userCollectionRepository,
			IMapper mapper)
			: base(repository, mapper)
		{
			_collectionRepository = repository;
			_comicCollectionRepository = comicCollectionRepository;
			_userCollectionRepository = userCollectionRepository;
			_mapper = mapper;
		}

		public IEnumerable<CollectionDto> GetAll()
		{
			var collections = _collectionRepository.GetAll();
			var result = Mapper.Map<IEnumerable<CollectionDto>>(collections);
			return result;
		}

		public IEnumerable<string> GetUserCollection(int userId)
		{
			IEnumerable<ComicCollection> comicCollection = _comicCollectionRepository.GetComicsCollection(userId);
			var collectionNames = new List<string>();
			foreach (var id in comicCollection.Select(cc => cc.Id))
			{
				var userCollection = _userCollectionRepository.GetUserCollectionByComicCollectionId(id);
				IEnumerable<string> currentUserCollectionNames = userCollection.Select(us => us.Collection).Select(c => c.Name).Distinct();
				collectionNames.AddRange(currentUserCollectionNames);
			}
			return collectionNames.Distinct();
		}

		public CollectionDto GetByName(string name)
		{
			return _mapper.Map<CollectionDto>(_collectionRepository.getByName(name));
		}

		public IEnumerable<ComicDto> getComicsForUser(int collectionId, int userId)
		{
			IEnumerable<UserCollection> userCollections = _userCollectionRepository.GetUserCollection(collectionId);
			var comicsCollection = new List<ComicDto>();
			foreach (var usercollection in userCollections.Where(uc => uc.CollectionId == collectionId))
			{
				var comicCollection = _comicCollectionRepository.GetWithDetails(usercollection.ComicCollectionId);
				var comic = _mapper.Map<ComicDto>(comicCollection.Comic);
				comicsCollection.Add(comic);
			}
			return comicsCollection;
		}
	}
}
