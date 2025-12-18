using BookTrack.Core.Entitites;
using BookTrack.Core.Enums;

namespace BookTrack.Shared.ViewModels.Books;

public class BookViewModel
{
    public BookViewModel(string title, string description, string isbn, string author, BookGenreEnum genre, int yearOfPublication, int numberOfPages, decimal averageRating, List<int> reviews)
    {
        Title = title;
        Description = description;
        ISBN = isbn;
        Author = author;
        Genre = genre;
        YearOfPublication = yearOfPublication;
        NumberOfPages = numberOfPages;
        AverageRating = averageRating;
        Reviews = reviews;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public BookGenreEnum Genre { get; set; }
    public int YearOfPublication { get; set; }
    public int NumberOfPages { get; set; }
    public decimal AverageRating  { get; set; }
    public List<int> Reviews { get; set; }
    
    
    
    public static BookViewModel FromEntity(Book book)
    {
        var reviews = new List<int>();
        if (book.Reviews is not null)
             reviews = book.Reviews.Select(e => e.Rating).ToList();
        
        return new BookViewModel(book.Title, book.Description, book.Isbn,book.Author, book.Genre, book.YearOfPublication,book.NumberOfPages,book.AverageRating, reviews);
    }
}