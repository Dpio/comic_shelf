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
		private readonly IMapper _mapper;

		public CollectionService(
			ICollectionRepository repository,
			IMapper mapper)
			: base(repository, mapper)
		{
			_collectionRepository = repository;
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
		    IEnumerable<Collection> comicsCollection = _collectionRepository.GetByUserId(userId);

		    IEnumerable<Comic> comics = comicsCollection.SelectMany(c => c.Comics);
		    return comics.Select(c => c.Title);
        }

		public CollectionDto GetByName(string name)
		{
			return _mapper.Map<CollectionDto>(_collectionRepository.GetByName(name));
		}

        // TODO I don't know why function name is GetComicsForUser and there is collectionId? It was needed?
        public IEnumerable<ComicDto> GetComicsForUser(int collectionId, int userId)
		{
		    IEnumerable<Collection> comicsCollection = _collectionRepository.GetByUserId(userId);

		    var comics = comicsCollection.SelectMany(c => c.Comics);
		    return _mapper.Map<IEnumerable<ComicDto>>(comics);
		}
	}
}
