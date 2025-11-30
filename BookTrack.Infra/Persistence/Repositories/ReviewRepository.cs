using BookTrack.Core.Entitites;
using BookTrack.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Infra.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly BookTrackDbContext _dbContext;

    public ReviewRepository(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Review>> GetAllByBookId(int bookId) => await _dbContext.Reviews.Where(x => x.IdBook == bookId).ToListAsync();

    public async Task<Review?> GetById(int id) => await _dbContext.Reviews.SingleOrDefaultAsync(x => x.Id == id);
    
    public async Task<int> Add(Review review){
    
        await _dbContext.Reviews.AddAsync(review);
        await UpdateBookAverageRating(review.IdBook);
        
        return review.Id;
    }

    public async Task UpdateBookAverageRating(int bookId)
    {
        var book = await _dbContext.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == bookId);
            
        if (book.Reviews.Any())
            book.AverageRating = (decimal)book.Reviews.Average(r => r.Rating);
    }

    public Task<bool> ReviewAlreadyExists(int bookId, int userId) =>  _dbContext.Reviews.AnyAsync(x => x.IdBook == bookId && x.IdUser == userId);
}