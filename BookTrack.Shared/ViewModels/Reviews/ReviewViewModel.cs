using BookTrack.Core.Entitites;

namespace BookTrack.Shared.ViewModels.Reviews;

public class ReviewViewModel
{
    public ReviewViewModel(int rating, string description, int idUser, int idBook)
    {
        Rating = rating;
        Description = description;
        IdUser = idUser;
        IdBook = idBook;
    }

    public int Rating { get; set; }
    public string Description { get; set; }
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    
    public static ReviewViewModel FromEntity(Review review)
    {
        return new ReviewViewModel(review.Rating, review.Description, review.IdUser, review.IdBook);
    }
}