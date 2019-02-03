using System.Collections.Generic;
using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Logic.Base;
using ComicShelf.Models.Comic;

namespace ComicShelf.Logic.Impl
{
	public class ComicService : CrudAppService<Comic, ComicDto, CreateComicDto, ComicDto>, IComicService
	{
		private readonly IComicRepository _comicRepository;

		public ComicService(IComicRepository repository,IMapper mapper) : base (repository,mapper)
		{
			_comicRepository = repository;
		}

		public ComicDto Get(int id)
		{
			var comic = _comicRepository.GetAsync(id).GetAwaiter().GetResult();
			var result = Mapper.Map<ComicDto>(comic);
			return result;
		}
		public IEnumerable<ComicDto> GetAll()
		{
			var comics = _comicRepository.GetAllComics();
			var result = Mapper.Map<IEnumerable<ComicDto>>(comics);
			return result;
		}

		public IEnumerable<ComicDto> GetComicsCollection(int userId)
		{
			var comics = _comicRepository.GetComicsCollection(userId);
			var result = Mapper.Map<IEnumerable<ComicDto>>(comics);
			return result;
		}
	}
}
