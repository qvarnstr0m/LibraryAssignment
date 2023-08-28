using LibraryAssignment.Data.DbContext;
using LibraryAssignment.MinimalApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAssignment.MinimalApi.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T?> Create(T entity)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> Get(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T?>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> Update(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return null;
        }
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return null;
        }
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}