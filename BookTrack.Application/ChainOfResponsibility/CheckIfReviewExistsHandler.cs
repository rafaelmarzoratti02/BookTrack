using BookTrack.Core.Exceptions;
using BookTrack.Core.Repositories;
using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public class CheckIfReviewExistsHandler : ReviewHandler, IReviewHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckIfReviewExistsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(CreateReviewInputModel model)
    {
        var reviewExists = await  _unitOfWork.Reviews.ReviewAlreadyExists(model.IdBook, model.IdUser);
        if (reviewExists)
            throw new ReviewAlreadyExistsException();

        await base.Handle(model);
    }
}