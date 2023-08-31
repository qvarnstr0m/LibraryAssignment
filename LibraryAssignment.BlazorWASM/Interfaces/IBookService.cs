using LibraryAssignment.Data.DTOs;
using LibraryAssignment.Data.Models;

namespace LibraryAssignment.BlazorWASM.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>?> GetBooksAsync();
    Task<Book?> GetBookAsync(int id);
    Task<bool> AddBookAsync(CreateBookDto book);
    Task<(bool isSuccess, string message)> UpdateBookAsync(int id, UpdateBookDto book);
    Task<bool> DeleteBookAsync(int id);
    Task<IEnumerable<Book>?> SearchBooksAsync(string searchTerm);
}