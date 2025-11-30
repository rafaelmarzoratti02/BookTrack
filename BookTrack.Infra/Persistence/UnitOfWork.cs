using BookTrack.Core.Repositories;

namespace BookTrack.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    
    private  readonly BookTrackDbContext _dbContext;

    public UnitOfWork(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}