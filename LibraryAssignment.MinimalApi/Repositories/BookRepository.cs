using LibraryAssignment.Data.DbContext;
using LibraryAssignment.Data.Models;
using LibraryAssignment.MinimalApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAssignment.MinimalApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Book> _dbSet;
    private readonly ILogger<BookRepository> _logger;
    
    public BookRepository(AppDbContext dbContext, ILogger<BookRepository> logger)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Book>();
        _logger = logger;
    }
    
    public async Task<IEnumerable<Book>> SearchBooks(string searchTerm)
    {
        return await _dbSet.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm)).ToListAsync();
    }
}