using BookTrack.Shared.ViewModels.Books;
using MediatR;

namespace BookTrack.Application.Queries.BookQueries.GetBookById;

public class GetBookByIdQuery : IRequest<BookViewModel>
{
    public GetBookByIdQuery(int bookId)
    {
        BookId = bookId;
    }

    public int BookId { get; set; }
}