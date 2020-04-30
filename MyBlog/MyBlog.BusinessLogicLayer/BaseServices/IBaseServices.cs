using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.BusinessLogicLayer.BaseServices
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        int Add(TEntity entity);

        Task<int> AddAsync(TEntity entity);

        int AddRange(IEnumerable<TEntity> entities);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        bool Delete(object id);

        Task<bool> DeleteAsync(object id);

        bool DeleteRange(IEnumerable<TEntity> entities);

        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);

        long Count();

        Task<long> CountAsync();

        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);

        Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
