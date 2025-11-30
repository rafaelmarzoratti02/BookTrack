using BookTrack.Core.Entitites;

namespace BookTrack.Shared.InputModels.Reviews;

public class CreateReviewInputModel
{
    public int Rating { get; set; }
    public string Description { get; set; }
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime ReadingStartDate { get; set; }
    public DateTime ReadingEndDate  { get; set; }
    
    public Review ToEntity()
        => new(Rating, Description, IdUser, IdBook,  DateCreated, ReadingStartDate, ReadingEndDate);
}