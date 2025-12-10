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

        var bookExists = await _unitOfWork.Books.Exists(model.IdBook);
        var userExists = await _unitOfWork.Users.Exists(model.IdUser);
        
        if (!bookExists || !userExists)
            throw new IdNotFoundOnInsertReviewException();
        // juntar em um validacao so?
        var reviewExists = await  _unitOfWork.Reviews.ReviewAlreadyExists(model.IdBook, model.IdUser);
        if (reviewExists)
            throw new ReviewAlreadyExistsException();
        
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