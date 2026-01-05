using BookTrack.Core.Repositories;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.ViewModels.Books;
using MediatR;

namespace BookTrack.Application.Queries.BookQueries.GetBookById;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookViewModel>
{
      
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book =  await _unitOfWork.Books.GetById(request.BookId);
        
        if(book is null)
            throw new NotFoundException();
        
        var model = BookViewModel.FromEntity(book);
        
        return model;
    }
}