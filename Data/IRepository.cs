using System;
using System.Collections.Generic;
using System.Linq;
namespace CardStorage.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Delete(params TEntity[] entities);

        Task<TEntity?> GetByIdAsync(int id);

        IQueryable<TEntity> GetAll();

        Task InsertAsync(params TEntity[] entities);

        void Update(params TEntity[] entities);

        Task SaveChangesAsync();
    }
}
