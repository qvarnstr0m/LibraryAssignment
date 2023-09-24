using LibraryAssignment.MinimalApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAssignment.MinimalApi.Services;

public class BookService : IBookService
{
    public async Task<IActionResult> StoreBookCoverImageAsync(string coverImageName, IFormFile coverImageFile)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", coverImageName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await coverImageFile.CopyToAsync(stream);
        return new OkResult();
    }
}