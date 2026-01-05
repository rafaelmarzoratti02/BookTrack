using BookTrack.Core.Repositories;
using BookTrack.Shared.ViewModels.Reviews;
using MediatR;

namespace BookTrack.Application.Queries.ReviewQueries.GetAllByBookId;

public class GetAllByBookIdHandler : IRequestHandler<GetAllByBookIdQuery, List<ReviewItemViewModel>>
{
    public GetAllByBookIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<List<ReviewItemViewModel>> Handle(GetAllByBookIdQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.Reviews.GetAllByBookId(request.BookId);
        var model = reviews.Select(x => ReviewItemViewModel.FromEntity(x)).ToList(); 
        return model;
    }
}