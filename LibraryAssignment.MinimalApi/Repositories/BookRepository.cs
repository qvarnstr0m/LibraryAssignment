using LibraryAssignment.Data.DbContext;
using LibraryAssignment.Data.Models;
using LibraryAssignment.MinimalApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAssignment.MinimalApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Book> _dbSet;
    
    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Book>();
    }
    
    public async Task<IEnumerable<Book>> SearchBooks(string searchTerm)
    {
        return await _dbSet.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm)).ToListAsync();
    }
}