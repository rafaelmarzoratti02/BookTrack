using BookTrack.Core.Entitites;
using BookTrack.Core.Events;
using BookTrack.Core.Repositories;

namespace BookTrack.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookTrackDbContext _dbContext;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public UnitOfWork(
        BookTrackDbContext dbContext,
        IBookRepository books,
        IReviewRepository reviews,
        IUserRepository users,
        IDomainEventDispatcher eventDispatcher)
    {
        _dbContext = dbContext;
        Books = books;
        Reviews = reviews;
        Users = users;
        _eventDispatcher = eventDispatcher;
    }

    public IBookRepository Books { get; }
    public IReviewRepository Reviews { get; }
    public IUserRepository Users { get; }

    public async Task<int> CompleteAsync()
    {
        var entitiesWithEvents = _dbContext.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();
        
        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }
        
        await _eventDispatcher.DispatchAsync(domainEvents);
        
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