using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UserCase.Core.Entities;

namespace UserCase.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext _dbContext;
        private DbSet<T> Set => _dbContext.Set<T>();

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T entity)
        {
            return Set.Add(entity).Entity;
        }

        public T Update(T entity)
        {
            var tracker = _dbContext.Entry(entity);
            tracker.State = EntityState.Modified;
            return tracker.Entity;
        }

        public bool Update(Expression<Func<T, bool>> condition, Action<T> updateFunc)
        {
            var entities = Set.Where(condition);
            foreach (var entity in entities)
            {
                updateFunc(entity);
            }

            return true;
        }

        public bool Delete(int id)
        {
            var found = Set.Find(id);
            if (found == null)
            {
                return false;
            }

            found.IsDeleted = true;
            return true;
        }

        public T Get(int id)
        {
            return Set.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public T Get(Expression<Func<T, bool>> condition)
        {
            return Set.Where(x => !x.IsDeleted).FirstOrDefault(condition);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> condition)
        {
            return Set.Where(x => !x.IsDeleted).Where(condition);
        }

        public IEnumerable<T> GetMany<TProperty>(Expression<Func<T, bool>> condition,
            Func<IQueryable<T>, IIncludableQueryable<T, TProperty>> queryable)
        {
            var query = Set.Where(x => !x.IsDeleted).Where(condition);
            query = queryable(query);
            return query;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}