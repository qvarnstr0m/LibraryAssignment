using Microsoft.AspNetCore.Mvc;

namespace LibraryAssignment.MinimalApi.Interfaces;

public interface IBookService
{
    Task<IActionResult> StoreBookCoverImageAsync(string coverImageName, IFormFile coverImageFile);
}