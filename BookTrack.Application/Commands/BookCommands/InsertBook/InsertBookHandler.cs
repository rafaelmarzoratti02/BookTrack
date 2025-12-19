using BookTrack.Core.Exceptions;
using BookTrack.Core.Repositories;
using MediatR;

namespace BookTrack.Application.Commands.BookCommands.AddBook;

public class InsertBookHandler : IRequestHandler<InsertBookCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public InsertBookHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async  Task<int> Handle(InsertBookCommand request, CancellationToken cancellationToken)
    {
        var isbnExists = await _unitOfWork.Books.IsbnExists(request.ISBN);
        if(isbnExists)
            throw new IsbnAlreadyExistsException();
        
        var book = request.ToEntity();
        await _unitOfWork.Books.Add(book);
        await _unitOfWork.CompleteAsync();
       
        return book.Id;
    }
}