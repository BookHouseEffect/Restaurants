using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using Restaurants.API.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

		public async Task AddAsync(TEntity item, long modifierId)
		{
			DateTimeOffset now = DateTimeOffset.UtcNow;
			item.CreatedByUserId = modifierId;
			item.CreatedDateTime = now;
			item.ModifiedByUserId = modifierId;
			item.ModifiedDateTime = now;

			await DbSet.AddAsync(item);
			await _dbContext.SaveChangesAsync();

			await _dbContext.Entry(item).ReloadAsync();
			await _dbContext.Entry(item).Reference(x => x.CreatedBy).LoadAsync();
			await _dbContext.Entry(item).Reference(x => x.ModifiedBy).LoadAsync();
		}

		public Task<TEntity> FindById(long id)
        {
			return DbSet.Where(x => x.Id == id)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.SingleOrDefaultAsync();
        }

        public Task<List<TEntity>> GetAll()
        {
            return DbSet
                .AsNoTracking()
				.Include(x=>x.CreatedBy)
				.Include(x=>x.ModifiedBy)
                .ToListAsync();
        }

        public Task<List<TEntity>> GetPaged(int pageNumber, int pageSize)
        {
            return DbSet
                .AsNoTracking()
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

		public async Task RemoveAsync(TEntity item)
		{
			DbSet.Attach(item);
			DbSet.Remove(item);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(TEntity item, long modifierId)
		{
			DateTimeOffset now = DateTimeOffset.UtcNow;
			item.ModifiedByUserId = modifierId;
			item.ModifiedDateTime = now;

			_dbContext.Entry(item).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			await _dbContext.Entry(item).ReloadAsync();
			await _dbContext.Entry(item).Reference(x => x.CreatedBy).LoadAsync();
			await _dbContext.Entry(item).Reference(x => x.ModifiedBy).LoadAsync();
		}
	}
}
