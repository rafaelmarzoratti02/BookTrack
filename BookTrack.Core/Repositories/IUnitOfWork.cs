namespace BookTrack.Core.Repositories;

public interface IUnitOfWork
{
    IBookRepository Books { get; }
    IReviewRepository Reviews { get; }
    IUserRepository Users { get; }
    
    Task<int> CompleteAsync();
}