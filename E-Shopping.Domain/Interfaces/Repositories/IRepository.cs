using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shopping.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Temel CRUD işlemleri
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        // Sorgulama
        Task<T?> GetSingleAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetByFilterWithIncludesAsync(
          Expression<Func<T, bool>> predicate,
          params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
    }
}