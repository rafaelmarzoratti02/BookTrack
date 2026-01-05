using BookTrack.Application.ChainOfResponsibility;
using BookTrack.Core.Exceptions;
using BookTrack.Core.Repositories;
using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Reviews;
using BookTrack.Shared.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Application.Services;

public class ReviewService : IReviewService
{
    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;

    public async Task<int> Insert(CreateReviewInputModel model)
    {
        var review = model.ToEntity();
        
        // var validateUserHandler = new ValidateUserHandler(_unitOfWork);
        // var validateBookHandler = new ValidateBookHandler(_unitOfWork);
        // var checkIfReviewExistsHandler = new CheckIfReviewExistsHandler(_unitOfWork);

        // validateUserHandler
        //     .SetNext(validateBookHandler)
        //     .SetNext(checkIfReviewExistsHandler);
        //
        // await validateUserHandler.Handle(model);

        await _unitOfWork.Reviews.Add(review);
        await _unitOfWork.CompleteAsync();
        
        return review.Id;
    }

    public async Task<List<ReviewViewModel>> GetAllByBookId(int bookId)
    {
        var reviews = await _unitOfWork.Reviews.GetAllByBookId(bookId);
        var model = reviews.Select(x => ReviewViewModel.FromEntity(x)).ToList(); 
        return model;
    }

    public async Task<ReviewViewModel> GetById(int reviewId)
    {
        var review = await _unitOfWork.Reviews.GetById(reviewId);
        if (review is null)
            throw new NotFoundException();
        
        var model = ReviewViewModel.FromEntity(review);
        return model;
    }
    
}