using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;

namespace Restaurants.API.Persistence
{
    interface IRepository<T> where T : BaseEntity
    {
        void Add(
            T item,
			long modifierId
        );

        T FindById(
            long id
        );

        IEnumerable<T> GetAll();

        IEnumerable<T> GetPaged(
            int pageNumber,
            int pageSize
        );

        IEnumerable<T> Get(
            Func<T, bool> predicate
        );

        void Remove(
            T item
        );

        void Update(
            T item,
			long modiierId
        );
    }
}
