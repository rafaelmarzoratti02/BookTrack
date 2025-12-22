using BookTrack.Core.Repositories;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public class ValidateUserHandler : OrderHandler, IOrderHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public ValidateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(CreateReviewInputModel model)
    {
        var userExists = await  _unitOfWork.Users.Exists(model.IdUser);
        if (!userExists)
            throw new IdNotFoundOnInsertReviewException();
        await base.Handle(model);
    }
}