using BookTrack.Shared.InputModels.Books;
using MediatR;

namespace BookTrack.Application.Queries.BookQueries.GetAllBooks;

public class GetAllBooksQuery : IRequest<List<BookItemViewModel>>
{
    
}