using BookTrack.Core.Repositories;

namespace BookTrack.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookTrackDbContext _dbContext;
    public UnitOfWork(
        BookTrackDbContext dbContext, IBookRepository books, IReviewRepository reviews, IUserRepository users)
    {
        _dbContext = dbContext;
        Books = books;
        Reviews = reviews;
        Users = users;
    }

    public IBookRepository Books { get; }
    public IReviewRepository Reviews { get; }
    public IUserRepository Users { get; }
    
    public async Task<int> CompleteAsync()
    {
       return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _dbContext.Dispose();

    }
}