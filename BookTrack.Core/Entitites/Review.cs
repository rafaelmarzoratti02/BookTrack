namespace BookTrack.Core.Entitites;

public class Review : BaseEntity
{
    public Review(int rating, string description, int idUser, int idBook, DateTime readingStartDate, DateTime readingEndDate) :base()
    {
        Rating = rating;
        Description = description;
        IdUser = idUser;
        IdBook = idBook;
        ReadingStartDate = readingStartDate;
        ReadingEndDate = readingEndDate;
    }

    public int Rating { get; set; }
    public string Description { get; set; }
    public int IdUser { get; set; }
    public User User { get; set; }
    public int IdBook { get; set; }
    public Book Book { get; set; }
    public DateTime ReadingStartDate { get; set; }
    public DateTime ReadingEndDate  { get; set; }
}