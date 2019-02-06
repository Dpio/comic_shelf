using System.Collections.Generic;
using System.Linq;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;
using ComicShelf.Models.ComicCollection;

namespace ComicShelf.Logic.Impl
{
	public class ComicCollectionService : IComicCollectionService
	{
		private readonly IComicCollectionRepository _comicCollectionRepository;

		public ComicCollectionService(IComicCollectionRepository comicCollectionRepository)
		{
			_comicCollectionRepository = comicCollectionRepository;
		}

		public ComicCollection AddToCollection(ComicCollectionDto input)
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
			return comicCollection;
		}	

		public IEnumerable<ComicCollection> GetComicsCollection(int userId)
		{
			return _comicCollectionRepository.GetComicsCollection(userId);
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
