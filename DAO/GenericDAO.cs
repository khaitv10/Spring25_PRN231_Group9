﻿using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAO
{
    public class GenericDAO<TEntity> where TEntity : class
    {
        private readonly CvstsystemDbContext context;
        private readonly DbSet<TEntity> dbSet;
        private static GenericDAO<TEntity> instance = null;

        public static GenericDAO<TEntity> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GenericDAO<TEntity>();
                }
                return instance;
            }
        }

        public GenericDAO()
        {
            context = new CvstsystemDbContext();
            this.dbSet = context.Set<TEntity>();
        }


        private IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null)
        {
            var query = GetQueryable(filter, orderBy, includeProperties, pageIndex, pageSize);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var query = GetQueryable(filter, orderBy, includeProperties, null, null);
            if (orderBy != null)
                query = orderBy(query);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetSingleAirplane(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    string includeProperties = "")
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            // Nạp các thuộc tính liên quan từ includeProperties
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync();
        }


        public async Task<TEntity> GetByID(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task InsertRange(List<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }

        public async Task UpdateRange(List<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

    }
}
