using BookTrack.Shared.ViewModels.Reviews;
using MediatR;

namespace BookTrack.Application.Queries.ReviewQueries.GetReviewById;

public class GetReviewByIdQuery : IRequest<ReviewViewModel>
{
    public GetReviewByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}