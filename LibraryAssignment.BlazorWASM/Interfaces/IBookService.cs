using LibraryAssignment.BlazorWASM.DTOs;
using LibraryAssignment.Data.Models;

namespace LibraryAssignment.BlazorWASM.Interfaces;

public interface IBookService
{
    Task<(bool isSuccess, IEnumerable<Book?>? books, string message)> GetBooksAsync();
    Task<(bool isSuccess, Book? book, string message)> GetBookAsync(int id);
    Task<(bool isSuccess, string message)> CreateBookAsync(CreateBookDto book);
    Task<(bool isSuccess, string message)> UpdateBookAsync(int id, UpdateBookDto book);
    Task<(bool isSuccess, string message)> DeleteBookAsync(int id);
    Task<(bool isSuccess, IEnumerable<Book>? books)> SearchBooksAsync(string searchTerm);
}