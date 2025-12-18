using BookTrack.Core.Enums;

namespace BookTrack.Core.Entitites;

public class Book : BaseEntity
{
    
    public Book(string title, string description, string isbn, string author, BookGenreEnum genre, int yearOfPublication, int numberOfPages)
    {
        Title = title;
        Description = description;
        ISBN = isbn;
        Author = author;
        Genre = genre;
        YearOfPublication = yearOfPublication;
        NumberOfPages = numberOfPages;
        AverageRating = 0;
        Reviews = [];
    }


    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public BookGenreEnum Genre { get; set; }
    public int YearOfPublication { get; set; }
    public int NumberOfPages { get; set; }
    public decimal AverageRating { get; set; }
    public List<Review> Reviews { get; set; }

    public void Update(string title, string description, int yearOfPublication)
    {
        Title = title;
        Description = description; 
        YearOfPublication = yearOfPublication;
    }
}

