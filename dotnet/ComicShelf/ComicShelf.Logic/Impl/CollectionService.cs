﻿using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.Collection;
using ComicShelf.Models.Comic;
using ComicShelf.Models.ComicCollection;
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

		public CollectionService(
			ICollectionRepository repository,
			IMapper mapper,
			IUserRepository userRepository,
			IComicCollectionRepository comicCollectionRepository)
			: base(repository, mapper)
		{
			_collectionRepository = repository;
			_mapper = mapper;
			_userRepository = userRepository;
			_comicCollectionRepository = comicCollectionRepository;
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
			return Mapper.Map<ComicCollectionDto>(comicCollection);
		}

		public CollectionDto GetCollectionByName(string name, int userId)
		{
			var collection = _collectionRepository.GetCollectionByName(name, userId);
			return Mapper.Map<CollectionDto>(collection);
		}
	}
}