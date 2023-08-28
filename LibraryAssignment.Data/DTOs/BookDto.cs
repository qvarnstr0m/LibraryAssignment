namespace LibraryAssignment.Data.DTOs;

public class BookDto
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
}