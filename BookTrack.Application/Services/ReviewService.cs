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
    public ReviewService(IReviewRepository reviewRepository, IBookRepository bookRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<int> Insert(CreateReviewInputModel model)
    {
        var review = model.ToEntity();

        var bookExists = await _bookRepository.Exists(model.IdBook);
        var userExists = await _userRepository.Exists(model.IdUser);
        
        if (!bookExists || !userExists)
            throw new IdNotFoundOnInsertReviewException();
        
        var reviewExists = await _reviewRepository.ReviewAlreadyExists(model.IdBook, model.IdUser);
        if (reviewExists)
            throw new ReviewAlreadyExistsException();
        
        await _reviewRepository.Add(review);
        await _unitOfWork.SaveChanges();
        
        return review.Id;
    }

    public async Task<List<ReviewViewModel>> GetAllByBookId(int bookId)
    {
        var reviews = await _reviewRepository.GetAllByBookId(bookId);
        var model = reviews.Select(x => ReviewViewModel.FromEntity(x)).ToList(); 
        return model;
    }

    public async Task<ReviewViewModel> GetById(int reviewId)
    {
        var review = await _reviewRepository.GetById(reviewId);
        if (review is null)
            throw new NotFoundException();
        
        var model = ReviewViewModel.FromEntity(review);
        return model;
    }
    
}