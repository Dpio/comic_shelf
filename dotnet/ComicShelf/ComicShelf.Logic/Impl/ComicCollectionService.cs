using System.Collections.Generic;
using System.Linq;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Helpers;

namespace ComicShelf.Logic.Impl
{
	public class ComicCollectionService : IComicCollectionService
	{
		private readonly IComicCollectionRepository _comicCollectionRepository;

		public ComicCollectionService(IComicCollectionRepository comicCollectionRepository)
		{
			_comicCollectionRepository = comicCollectionRepository;
		}

		public ComicCollection AddToCollection(int userId, int comicId)
		{
			if (_comicCollectionRepository.GetAll().Any(x => x.UserId == userId && x.ComicId == comicId))
				throw new AppException(" Already in collection");
			var comicCollection = new ComicCollection()
			{
				ComicId = comicId,
				UserId = userId
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
