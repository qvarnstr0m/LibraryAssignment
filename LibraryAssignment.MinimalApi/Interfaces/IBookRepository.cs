using LibraryAssignment.Data.Models;

namespace LibraryAssignment.MinimalApi.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> SearchBooks(string searchTerm);
}