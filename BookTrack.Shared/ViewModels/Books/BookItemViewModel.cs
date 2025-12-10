using BookTrack.Core.Entitites;

namespace BookTrack.Shared.InputModels.Books;

public class BookItemViewModel
{
    public BookItemViewModel(int id,string title, string author, int yearOfPublication,  decimal averageRating)
    {
        Id = id;
        Title = title;
        Author = author;
        YearOfPublication = yearOfPublication;
        AverageRating = averageRating;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearOfPublication { get; set; }
    public decimal AverageRating { get; set; }
    
    public static BookItemViewModel FromEntity(Book book)
    {
        return new BookItemViewModel(book.Id, book.Title, book.Author, book.YearOfPublication, book.AverageRating);
    }
}