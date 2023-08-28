using System.Net;

namespace LibraryAssignment.MinimalApi.Models;

public class CustomResponse
{
    public bool IsSuccess { get; set; } = true;
    
    public List<string> ErrorMessages { get; set; } = new();
    public object? Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}