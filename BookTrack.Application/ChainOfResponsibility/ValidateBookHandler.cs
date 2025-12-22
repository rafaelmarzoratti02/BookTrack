using BookTrack.Core.Repositories;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public class ValidateBookHandler : OrderHandler, IOrderHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public ValidateBookHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(CreateReviewInputModel model)
    {
        var bookExists = await  _unitOfWork.Books.Exists(model.IdBook);
        if (!bookExists)
            throw new IdNotFoundOnInsertReviewException(); 

        await base.Handle(model);
    }
}