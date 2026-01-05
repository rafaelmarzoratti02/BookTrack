using BookTrack.Shared.ViewModels.Reviews;
using MediatR;

namespace BookTrack.Application.Queries.ReviewQueries.GetAllByBookId;

public class GetAllByBookIdQuery : IRequest<List<ReviewItemViewModel>>
{
    public GetAllByBookIdQuery(int bookId)
    {
        BookId = bookId;
    }

    public int  BookId { get; set; }
}