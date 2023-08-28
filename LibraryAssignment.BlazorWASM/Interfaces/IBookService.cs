using LibraryAssignment.Data.DTOs;
using LibraryAssignment.Data.Models;

namespace LibraryAssignment.BlazorWASM.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooks();
    Task<Book> GetBook(int id);
    Task<bool> AddBook(CreateBookDto book);
    Task<bool> UpdateBook(int id, UpdateBookDto book);
    Task<bool> DeleteBook(int id);
}