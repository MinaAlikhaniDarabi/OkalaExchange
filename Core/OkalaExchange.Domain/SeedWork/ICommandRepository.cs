using OkalaExchange.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Domain.SeedWork
{
    public interface ICommandRepository<TEntity> : IUnitOfWork where TEntity : AggregateRoot
    {
        void Delete(long id);

        void Delete(TEntity entity);

        void Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Get(long id);

        Task<TEntity> GetAsync(long id);

        bool Exists(Expression<Func<TEntity, bool>> expression);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
