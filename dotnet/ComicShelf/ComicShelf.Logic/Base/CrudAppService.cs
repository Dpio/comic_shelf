using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.DataAccess.Repositories;
using ComicShelf.Models.Base;

namespace ComicShelf.Logic.Base
{
	public abstract class CrudAppService<TEntity, TEntityDto, TCreateInput, TUpdateInput>
		: ICrudAppService<TEntityDto, TCreateInput, TUpdateInput>
		where TEntity : class, IEntity
		where TEntityDto : IEntityDto
		where TUpdateInput : IEntityDto
	{
		private readonly IGenericRepository<TEntity> _repository;
		protected readonly IMapper Mapper;

		protected CrudAppService(IGenericRepository<TEntity> repository, IMapper mapper)
		{
			Mapper = mapper;
			_repository = repository;
		}

		public virtual TEntityDto Get(int id)
		{
			var entity = GetEntityById(id);
			return Mapper.Map<TEntityDto>(entity);
		}

		public virtual TEntityDto Create(TCreateInput input)
		{
			var entity = Mapper.Map<TEntity>(input);

			_repository.Add(entity);
			_repository.SaveChanges();

			return Mapper.Map<TEntityDto>(entity);
		}

		public virtual TEntityDto Update(TUpdateInput input)
		{
			var entity = GetEntityById(input.Id);

			Mapper.Map(input, entity);
			_repository.SaveChanges();

			return Mapper.Map<TEntityDto>(entity);
		}

		public virtual void Delete(int id)
		{
			_repository.Remove(id);
			_repository.SaveChanges();
		}

		protected virtual TEntity GetEntityById(int id)
		{
			return _repository.Get(id);
		}
	}
}
