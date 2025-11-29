using BookTrack.Core.Enums;

namespace BookTrack.Core.Entitites;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public BookGenreEnum Genre { get; set; }
    public int YearOfPublication { get; set; }
    public int NumberOfPages { get; set; }
    public DateTime AverageRating { get; set; }
    public byte BookCover { get; set; }
    public List<Review> Reviews { get; set; }
}

