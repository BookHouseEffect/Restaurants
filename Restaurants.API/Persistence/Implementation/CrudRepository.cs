using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using Restaurants.API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.API.Persistence.Implementation
{
    public abstract class CrudRepository<TEntity> :
        BaseRepository,
        IRepository<TEntity> where TEntity : BaseEntity
    {

        protected DbSet<TEntity> DbSet;

        public EntityState Modified { get; private set; }

        public CrudRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbSet = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity item, long modifierId)
        {
			DateTimeOffset now = DateTimeOffset.UtcNow;
			item.CreatedByUserId = modifierId;
			item.CreatedDateTime = now;
			item.ModifiedByUserId = modifierId;
			item.ModifiedDateTime = now;

            DbSet.Add(item);
            _dbContext.SaveChanges();

			_dbContext.Entry(item).Reload();
			_dbContext.Entry(item).Reference(x => x.CreatedBy).Load();
			_dbContext.Entry(item).Reference(x => x.ModifiedBy).Load();
		}

        public TEntity FindById(long id)
        {
			return DbSet.Where(x => x.Id == id)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.SingleOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet
                .AsNoTracking()
				.Include(x=>x.CreatedBy)
				.Include(x=>x.ModifiedBy)
                .ToList();
        }

        public IEnumerable<TEntity> GetPaged(int pageNumber, int pageSize)
        {
            return DbSet
                .AsNoTracking()
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return DbSet
                .AsNoTracking()
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Where(predicate)
                .ToList();
        }

        public void Remove(TEntity item)
        {
            DbSet.Attach(item);
            DbSet.Remove(item);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity item, long modifierId)
        {
			DateTimeOffset now = DateTimeOffset.UtcNow;
			item.ModifiedByUserId = modifierId;
			item.ModifiedDateTime = now;

			_dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();

			_dbContext.Entry(item).Reload();
			_dbContext.Entry(item).Reference(x => x.CreatedBy).Load();
			_dbContext.Entry(item).Reference(x => x.ModifiedBy).Load();
		}
    }
}
