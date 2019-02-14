using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.ComicCollection;

namespace ComicShelf.Logic.Impl
{
	public class ComicCollectionService : IComicCollectionService
	{
		private readonly IComicCollectionRepository _comicCollectionRepository;
		private readonly IMapper _mapper; 

		public ComicCollectionService(IComicCollectionRepository comicCollectionRepository, IMapper mapper)
		{
			_comicCollectionRepository = comicCollectionRepository;
			_mapper = mapper;
		}

		public ComicCollectionDto AddToCollection(CreateComicCollectionDto input)
		{
			if (_comicCollectionRepository.GetAll().Any(x => x.UserId == input.UserId && x.ComicId == input.ComicId))
				throw new AppException(" Already in collection");
			var comicCollection = new ComicCollection()
			{
				ComicId = input.ComicId,
				UserId = input.UserId
			};
			_comicCollectionRepository.Add(comicCollection);
			_comicCollectionRepository.SaveChanges();
			var comicCollectionDto = _mapper.Map<ComicCollectionDto>(comicCollection);
			return comicCollectionDto;
		}	

		public IEnumerable<ComicCollectionDto> GetComicsCollection(int userId)
		{
			var comicCollections = _comicCollectionRepository.GetComicsCollection(userId);
			var result = _mapper.Map<IEnumerable<ComicCollectionDto>>(comicCollections);
			return result;
		}

		public void DeleteComicFromCollection(int id)
		{
			var comic = _comicCollectionRepository.Get(id);
			if (comic != null)
			{
				_comicCollectionRepository.Remove(id);
				_comicCollectionRepository.SaveChanges();
			}
		}
	}
}
