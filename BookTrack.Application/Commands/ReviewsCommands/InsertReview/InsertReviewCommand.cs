using BookTrack.Core.Entitites;
using BookTrack.Shared.ViewModels.Reviews;
using MediatR;

namespace BookTrack.Application.Commands.ReviewsCommands.InsertReview;

public class InsertReviewCommand : IRequest<int>
{
    public int Rating { get; set; }
    public string Description { get; set; }
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    public DateTime ReadingStartDate { get; set; }
    public DateTime ReadingEndDate  { get; set; }
    
    public Review ToEntity()
        => new(Rating, Description, IdUser, IdBook, ReadingStartDate, ReadingEndDate);
    
}