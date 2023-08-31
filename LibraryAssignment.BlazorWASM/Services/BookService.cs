using System.Text;
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

    public async Task<IEnumerable<Book>?> GetBooksAsync()
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

    public async Task<Book?> GetBookAsync(int id)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _booksUrl + $"/{id}");
            var response = await _httpClient.SendAsync(request);
            var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return book;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> AddBookAsync(CreateBookDto book)
    {
        throw new NotImplementedException();
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

            return (false, "Error updating book:" + response.StatusCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, "Error updating book:" + e.Message);
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Book>?> SearchBooksAsync(string searchTerm)
    {
        try
        {
            string requestUri = $"{_booksUrl}/search?searchWord={searchTerm}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
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