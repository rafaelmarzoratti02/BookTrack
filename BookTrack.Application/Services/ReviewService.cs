using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Reviews;
using BookTrack.Shared.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Application.Services;

public class ReviewService : IReviewService
{
    private readonly BookTrackDbContext _dbContext;

    public ReviewService(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Insert(CreateReviewInputModel model)
    {
        var review = model.ToEntity();
        
        var bookExists = await _dbContext.Books.AnyAsync(x=> x.Id == review.IdBook);
        var userExists = await _dbContext.Users.AnyAsync(x=> x.Id == review.IdUser);
       
        
        if (!bookExists || !userExists)
            throw new IdNotFoundOnInsertReviewException();
        
        var reviewExists = await _dbContext.Reviews.AnyAsync(x=> x.IdBook == review.IdBook && x.IdUser == review.IdUser);

        if (reviewExists)
            throw new ReviewAlreadyExistsException();
        
        await _dbContext.Reviews.AddAsync(review);
        await UpdateBookAverageRating(review.IdBook);
        
        await _dbContext.SaveChangesAsync();
        
        return review.Id;
    }

    public async Task<List<ReviewViewModel>> GetAllByBookId(int bookId)
    {
        var reviews =  await _dbContext.Reviews.Where(x => x.IsActive).ToListAsync();
        var model = reviews.Select(x => ReviewViewModel.FromEntity(x)).ToList(); 
        return model;
    }

    public async Task<ReviewViewModel> GetById(int reviewId)
    {
        var review = await _dbContext.Reviews.FirstOrDefaultAsync(b => b.Id == reviewId);
        if (review is null)
            throw new NotFoundException();
        
        var model = ReviewViewModel.FromEntity(review);
        return model;
    }

    public async Task UpdateBookAverageRating(int bookId)
    {
        var book = await _dbContext.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == bookId);
            
        if (book == null)
            throw new NotFoundException();
            
        if (book.Reviews.Any())
            book.AverageRating = (decimal)book.Reviews.Average(r => r.Rating);

        
        
        await _dbContext.SaveChangesAsync();

    }
}