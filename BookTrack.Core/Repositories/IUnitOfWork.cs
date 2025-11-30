namespace BookTrack.Core.Repositories;

public interface IUnitOfWork
{
    Task SaveChanges();
}