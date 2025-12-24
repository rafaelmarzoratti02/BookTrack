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
    
    public async Task<int> Add(Review review)
    {
        await _dbContext.Reviews.AddAsync(review);
        return review.Id;
    }

    public Task<bool> ReviewAlreadyExists(int bookId, int userId) =>  _dbContext.Reviews.AnyAsync(x => x.IdBook == bookId && x.IdUser == userId);
}