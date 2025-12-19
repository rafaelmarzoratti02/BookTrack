using BookTrack.Core.Repositories;
using BookTrack.Shared.Exceptions;
using MediatR;

namespace BookTrack.Application.Commands.BookCommands.UpdateBook;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetById(request.IdBook);
        
        if(book is null)
            throw new NotFoundException();
        
        book.Update(request.Title, request.Description,request.YearOfPublication);
        
        await _unitOfWork.CompleteAsync();
    }
}