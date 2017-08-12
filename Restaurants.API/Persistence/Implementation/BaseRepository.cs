using Restaurants.API.Models.Context;
using System;

namespace Restaurants.API.Persistence.Implementation
{
    public class BaseRepository
    {
        protected readonly AppDbContext _dbContext;

        protected BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
