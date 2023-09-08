using LibraryAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAssignment.Data.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(mws => new { mws.Isbn })
            .IsUnique();
    }
}