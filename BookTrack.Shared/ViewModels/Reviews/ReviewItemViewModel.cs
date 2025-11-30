using BookTrack.Core.Entitites;

namespace BookTrack.Shared.ViewModels.Reviews;

public class ReviewItemViewModel
{
    public ReviewItemViewModel(string title, int rating)
    {
        Title = title;
        Rating = rating;
    }

    public string Title { get; set; }
    public int Rating { get; set; }
    
    public static ReviewItemViewModel FromEntity(Review review)
    {
        return new ReviewItemViewModel(review.Book.Title, review.Rating);
    }
}