using LibraryAssignment.BlazorWASM.Interfaces;
using LibraryAssignment.Data.DTOs;
using LibraryAssignment.Data.Models;
using Newtonsoft.Json;

namespace LibraryAssignment.BlazorWASM.Services;

public class BookService : IBookService

{
    private readonly HttpClient _httpClient;
    private readonly string _booksUrl = "https://localhost:7003/api/books";

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<Book>?> GetBooks()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _booksUrl);
            var response = await _httpClient.SendAsync(request);
            var bookList = JsonConvert.DeserializeObject<IEnumerable<Book>>(await response.Content.ReadAsStringAsync());
            return bookList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Book> GetBook(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddBook(CreateBookDto book)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateBook(int id, UpdateBookDto book)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBook(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Book>?> SearchBooks(string searchTerm)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_booksUrl}/search?searchWord={searchTerm}");
            Console.WriteLine($"{_booksUrl}/search?searchWord={searchTerm}");
            var response = await _httpClient.SendAsync(request);
            var bookList = JsonConvert.DeserializeObject<IEnumerable<Book>>(await response.Content.ReadAsStringAsync());
            return bookList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}