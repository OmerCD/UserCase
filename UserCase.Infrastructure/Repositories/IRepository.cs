using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using UserCase.Core.Entities;

namespace UserCase.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        bool Update(Expression<Func<TEntity, bool>> condition, Action<TEntity> updateFunc);
        bool Delete(int id);
        TEntity Get(int id);
        TEntity Get(Expression<Func<TEntity, bool>> condition);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition);

        public IEnumerable<TEntity> GetMany<TProperty>(Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> queryable);

        int SaveChanges();
    }
}