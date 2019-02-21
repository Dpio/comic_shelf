using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Models.Collection;
using System.Collections.Generic;
using System.Linq;

namespace ComicShelf.Logic.Impl
{
	public class CollectionService : CrudAppService<Collection, CollectionDto, CreateCollectionDto, CollectionDto>, ICollectionService
	{
		private readonly ICollectionRepository _collectionRepository;
		private readonly IComicCollectionRepository _comicCollectionRepository;
		private readonly IUserCollectionRepository _userCollectionRepository;

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
			foreach(var id in comicCollection.Select(cc => cc.Id))
			{
				var userCollection = _userCollectionRepository.GetUserCollectionByComicCollectionId(id);
				IEnumerable<string> currentUserCollectionNames = userCollection.Select(us => us.Collection).Select(c => c.Name).Distinct();
				collectionNames.AddRange(currentUserCollectionNames);
			}
			return collectionNames;
		}
	}
}
