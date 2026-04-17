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
        IQueryable<T> GetQueryable();

        // Basit filtre
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        //  Çoklu kayıt (filter + include)
        Task<List<T>> GetWhereWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes
        );

        // Tek kayıt (filter + include)
        Task<T> GetSingleWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes
        );
    }
}