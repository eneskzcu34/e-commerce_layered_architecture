using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using E_Shopping.Domain.Interfaces.Repositories;
using E_Shopping.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace E_Shopping.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result == null)
                throw new Exception($"{typeof(T).Name} with id {id} not found.");
            return result;
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsNoTracking();
        }


        public async Task<List<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetByFilterWithIncludesAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetSingleAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}