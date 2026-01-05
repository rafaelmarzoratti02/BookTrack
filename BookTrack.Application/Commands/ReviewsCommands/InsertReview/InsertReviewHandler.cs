using BookTrack.Application.ChainOfResponsibility;
using BookTrack.Core.Events;
using BookTrack.Core.Repositories;
using BookTrack.Shared.ViewModels.Reviews;
using MediatR;

namespace BookTrack.Application.Commands.ReviewsCommands.InsertReview;

public class InsertReviewHandler : IRequestHandler<InsertReviewCommand, int>
{
    public InsertReviewHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<int> Handle(InsertReviewCommand request, CancellationToken cancellationToken)
    {
        var review = request.ToEntity();
        review.AddDomainEvent(new UpdateAverageRatingDomainEvent(request.IdBook));

        var validateUserHandler = new ValidateUserHandler(_unitOfWork);
        var validateBookHandler = new ValidateBookHandler(_unitOfWork);
        var checkIfReviewExistsHandler = new CheckIfReviewExistsHandler(_unitOfWork);

        validateUserHandler
            .SetNext(validateBookHandler)
            .SetNext(checkIfReviewExistsHandler);

        await validateUserHandler.Handle(request);

        await _unitOfWork.Reviews.Add(review);
        await _unitOfWork.CompleteAsync();
        
        return review.Id;
    }
}