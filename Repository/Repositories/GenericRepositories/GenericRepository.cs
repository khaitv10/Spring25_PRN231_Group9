﻿using DAO;
using System.Linq.Expressions;

namespace Repository.Repositories.GenericRepositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null) => await GenericDAO<TEntity>.Instance.Get(filter, orderBy, includeProperties, pageIndex, pageSize);

        public async Task<TEntity> GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") => await GenericDAO<TEntity>.Instance.GetSingle(filter, orderBy, includeProperties);

        public async Task<TEntity> GetByID(int id) => await GenericDAO<TEntity>.Instance.GetByID(id);

        public async Task Insert(TEntity entity) => await GenericDAO<TEntity>.Instance.Insert(entity);

        public async Task InsertRange(List<TEntity> entities) => await GenericDAO<TEntity>.Instance.InsertRange(entities);

        public async Task Delete(TEntity entity) => await GenericDAO<TEntity>.Instance.Delete(entity);

        public async Task Update(TEntity entityToUpdate) => await GenericDAO<TEntity>.Instance.Update(entityToUpdate);

        public async Task<int> Count(Expression<Func<TEntity, bool>> filter = null) => await GenericDAO<TEntity>.Instance.Count(filter);

        public async Task UpdateRange(List<TEntity> entities) => await GenericDAO<TEntity>.Instance.UpdateRange(entities);

        public async Task DeleteRange(List<TEntity> entities) => await GenericDAO<TEntity>.Instance.DeleteRange(entities);


    }
}
