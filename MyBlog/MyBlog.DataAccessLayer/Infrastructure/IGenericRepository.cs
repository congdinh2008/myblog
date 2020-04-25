using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.DataAccessLayer.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add a(n) <see cref="TEntity"/> to database
        /// </summary>
        /// <param name="entity">Input a(n) <see cref="TEntity"/> as parameter</param>
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        long Count();

        Task<long> CountAsync();

        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
