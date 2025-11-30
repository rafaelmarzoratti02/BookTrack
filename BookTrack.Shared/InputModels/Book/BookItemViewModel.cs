using BookTrack.Core.Entitites;

namespace BookTrack.Shared.InputModels;

public class BookItemViewModel
{
    public BookItemViewModel(string title, string author, int yearOfPublication)
    {
        Title = title;
        Author = author;
        YearOfPublication = yearOfPublication;
    }

    public string Title { get; set; }
    public string Author { get; set; }
    public int YearOfPublication { get; set; }
    
    public static BookItemViewModel FromEntity(Book book)
    {
        return new BookItemViewModel(book.Title, book.Author, book.YearOfPublication);
    }
}