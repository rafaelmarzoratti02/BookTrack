using BookTrack.Core.Enums;

namespace BookTrack.Shared.ViewModels;

public class BookViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public BookGenreEnum Genre { get; set; }
    public int YearOfPublication { get; set; }
    public int NumberOfPages { get; set; }
}