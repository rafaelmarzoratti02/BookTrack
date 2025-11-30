using BookTrack.Core.Entitites;
using BookTrack.Core.Enums;

namespace BookTrack.Shared.InputModels;

public class CreateBookInputModel
{
    public CreateBookInputModel(string title, string description, string isbn, string author, BookGenreEnum genre, int yearOfPublication, int numberOfPages)
    {
        Title = title;
        Description = description;
        ISBN = isbn;
        Author = author;
        Genre = genre;
        YearOfPublication = yearOfPublication;
        NumberOfPages = numberOfPages;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public BookGenreEnum Genre { get; set; }
    public int YearOfPublication { get; set; }
    public int NumberOfPages { get; set; }
    
    public Book ToEntity()
        => new(Title, Description, ISBN, Author, Genre, YearOfPublication, NumberOfPages);
}