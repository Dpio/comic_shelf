using System;
using System.Collections.Generic;
using AutoMapper;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.Collection;
using ComicShelf.Models.ComicCollection;
using ComicShelf.Models.User;

namespace ComicShelf.Logic.Impl
{
	public class ComicCollectionService : IComicCollectionService
	{
		private readonly IUserRepository _userRepository;
	    private readonly ICollectionRepository _collectionRepository;
	    private readonly IMapper _mapper; 

		public ComicCollectionService(
			IMapper mapper,
			IUserRepository userRepository,
            ICollectionRepository collectionRepository
			)
		{
			_mapper = mapper;
			_userRepository = userRepository;
		    _collectionRepository = collectionRepository;
		}

		public CollectionDto AddToCollection(CreateComicCollectionDto input)
		{
			throw new NotImplementedException();
		}	

		public IEnumerable<CollectionDto> GetComicsCollection(int userId)
		{
			var comicCollections = _collectionRepository.GetByUserId(userId);
			var result = _mapper.Map<IEnumerable<CollectionDto>>(comicCollections);
			return result;
		}

		public void DeleteComicFromCollection(int id)
		{
		    throw new NotImplementedException();
        }
	}
}
