using Microsoft.EntityFrameworkCore;
using MyBlog.DataAccessLayer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.BusinessLogicLayer.BaseServices
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        public BaseServices(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        public int Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException();
            }

            Repository.Add(entity);

            return UnitOfWork.Commit();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException();
            }

            await Repository.AddAsync(entity);

            return await UnitOfWork.CommitAsync();
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new NullReferenceException();
            }

            Repository.AddRange(entities);

            return UnitOfWork.Commit();
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new NullReferenceException();
            }

            await Repository.AddRangeAsync(entities);

            return await UnitOfWork.CommitAsync();
        }

        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException();
            }

            Repository.Update(entity);

            return UnitOfWork.Commit() > 0;
        }

        public bool Delete(object id)
        {
            var entity = Repository.GetById(id);

            if (entity == null)
            {
                throw new NullReferenceException();
            }

            Repository.Delete(entity);

            return UnitOfWork.Commit() > 0;
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new NullReferenceException();
            }

            Repository.DeleteRange(entities);

            return UnitOfWork.Commit() > 0;
        }

        public long Count()
        {
            return Repository.Count();
        }

        public async Task<long> CountAsync()
        {
            return await Repository.CountAsync();
        }

        public TEntity GetById(object id)
        {
            return Repository.GetById(id);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public async Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10)
        {
            var query = Repository.Get(filter: filter, orderBy: orderBy, includeProperties: includeProperties);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await Paginated<TEntity>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Repository.Get().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await Repository.Get(filter: filter).ToListAsync();
        }
    }
}