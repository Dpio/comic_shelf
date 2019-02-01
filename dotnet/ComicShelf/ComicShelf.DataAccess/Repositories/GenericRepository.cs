using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicShelf.DataAccess.Repositories
{
	public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		protected DbSet<TEntity> Entities;
		private ApplicationDbContext _context;

		protected GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			Entities = context.Set<TEntity>();
		}

		public virtual TEntity Get(int id)
		{
			return Entities.Find(id);
		}

		public virtual async Task<TEntity> GetAsync(int id)
		{
			return await Entities.FindAsync(id);
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return Entities.OrderBy(e => e.Id).ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await Entities.ToListAsync();
		}

		public virtual IEnumerable<TEntity> GetRange(int start, int count)
		{
			return Entities.Skip(start).Take(count).ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(int start, int count)
		{
			return await Entities.Skip(start).Take(count).ToListAsync();
		}

		public virtual IEnumerable<TEntity> GetRange(int start, int count, Expression<Func<TEntity, bool>> predicate)
		{
			return Entities.Where(predicate).Skip(start).Take(count).ToList();
		}

		public virtual IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
		{
			var query = Entities.AsQueryable();
			if (propertySelectors != null)
			{
				foreach (var propertySelector in propertySelectors)
				{
					query = query.Include(propertySelector);
				}
			}
			return query.ToList();
		}

		public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return Entities.Where(predicate).ToList();
		}

		public virtual int Count()
		{
			return Entities.Count();
		}

		public virtual void Add(TEntity entity)
		{
			Entities.Add(entity);
		}

		public virtual void Remove(int id)
		{
			var entity = Get(id);
			Entities.Remove(entity);
		}
		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
