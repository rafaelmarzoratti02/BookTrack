using BookTrack.Core.Repositories;
using BookTrack.Shared.InputModels.Books;
using MediatR;

namespace BookTrack.Application.Queries.BookQueries.GetAllBooks;

public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookItemViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBooksHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<BookItemViewModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetAll();
        var model = books.Select(x => BookItemViewModel.FromEntity(x)).ToList();

        return model;
    }
}