using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence
{
    interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(
            T item,
			long modifierId
        );

        Task<T> FindById(
            long id
        );

        Task<List<T>> GetAll();

        Task<List<T>> GetPaged(
            int pageNumber,
            int pageSize
        );

        IEnumerable<T> Get(
            Func<T, bool> predicate
        );

        Task RemoveAsync(
            T item
        );

        Task UpdateAsync(
            T item,
			long modiierId
        );
    }
}
