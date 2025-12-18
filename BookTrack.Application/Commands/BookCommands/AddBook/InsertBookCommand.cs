using BookTrack.Core.Entitites;
using BookTrack.Core.Enums;
using MediatR;

namespace BookTrack.Application.Commands.BookCommands.AddBook;

public class InsertBookCommand : IRequest<int>
{
    public InsertBookCommand(string title, string description, string isbn, string author, BookGenreEnum genre, int yearOfPublication, int numberOfPages)
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