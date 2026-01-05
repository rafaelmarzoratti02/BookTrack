using BookTrack.Core.Repositories;
using BookTrack.Shared.Exceptions;
using MediatR;

namespace BookTrack.Application.Commands.BookCommands.DeleteBook;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetById(request.BookId);
        if(book is null)
            throw new NotFoundException();
        
        book.SetAsDeleted();
        
        await _unitOfWork.CompleteAsync();
    }
}