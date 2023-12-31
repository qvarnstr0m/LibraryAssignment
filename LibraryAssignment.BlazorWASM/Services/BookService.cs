using System.Text;
using LibraryAssignment.BlazorWASM.Interfaces;
using LibraryAssignment.BlazorWASM.DTOs;
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

    public async Task<(bool isSuccess, IEnumerable<Book?>? books, string message)> GetBooksAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _booksUrl);
            var response = await _httpClient.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)
            {
                return (false, null, "Error retrieving books, status code: " + response.StatusCode);
            }
            
            var bookList = JsonConvert.DeserializeObject<IEnumerable<Book>>(await response.Content.ReadAsStringAsync());
            return (true, bookList, "Books retrieved successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, null, "Internal server error: " + e.Message);
        }
    }

    public async Task<(bool isSuccess, Book? book, string message)> GetBookAsync(int id)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _booksUrl + $"/{id}");
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return (false, null, "Book not found, status code: " + response.StatusCode);
            }

            var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return (true, book, "Book found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, null, "Internal server error: " + e.Message);
        }
    }

    public async Task<(bool isSuccess, string message)> CreateBookAsync(CreateBookDto book)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _booksUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Book created successfully");
            }

            return (false, "Error creating book. Error message: " + response.StatusCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, "Internal server error: " + e.Message);
        }
    }

    public async Task<(bool isSuccess, string message)> UpdateBookAsync(int id, UpdateBookDto book)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _booksUrl + $"/{id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Book updated successfully");
            }

            return (false, "Error updating book. Error message: " + response.StatusCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, "Internal server error: " + e.Message);
        }
    }

    public async Task<(bool isSuccess, string message)> DeleteBookAsync(int id)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _booksUrl + $"/{id}");
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Book deleted successfully");
            }

            return (false, "Error deleting book. Error message: " + response.StatusCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, "Internal server error: " + e.Message);
        }
    }

    public async Task<(bool isSuccess, IEnumerable<Book>? books)> SearchBooksAsync(string searchTerm)
    {
        try
        {
            string requestUri = $"{_booksUrl}/search?searchWord={searchTerm}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await _httpClient.SendAsync(request);
            var bookList = JsonConvert.DeserializeObject<IEnumerable<Book>>(await response.Content.ReadAsStringAsync());
            return (true, bookList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, null);
        }
    }
}