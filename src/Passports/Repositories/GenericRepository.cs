using Microsoft.EntityFrameworkCore;
using Passports.Data;
using Passports.Models;
using Passports.Repositories.Interfaces;

namespace Passports.Repositories; 

public class GenericRepository<T> : IGenericRepository<T> where T : class {
    private protected PassportContext _context;
    private protected DbSet<T> _dbSet;
    public GenericRepository() {
    }
    public GenericRepository(PassportContext context) {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public virtual async Task<IEnumerable<T>> All() {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetById(int id) {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(T entity) {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> Update(T entity) {
        _dbSet.Update(entity);
        return true;
    }

    public virtual async Task<bool> Delete(T entity) {
        _dbSet.Remove(entity);
        return true;
    }
}