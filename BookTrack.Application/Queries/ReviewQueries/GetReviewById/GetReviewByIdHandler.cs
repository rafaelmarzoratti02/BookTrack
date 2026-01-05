using BookTrack.Core.Repositories;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.ViewModels.Reviews;
using MediatR;

namespace BookTrack.Application.Queries.ReviewQueries.GetReviewById;

public class GetReviewByIdHandler :  IRequestHandler<GetReviewByIdQuery, ReviewViewModel>
{
    public GetReviewByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async  Task<ReviewViewModel> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.Reviews.GetById(request.Id);
        if (review is null)
            throw new NotFoundException();
        
        var model = ReviewViewModel.FromEntity(review);
        return model;
    }
}