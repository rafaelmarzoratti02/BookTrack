using BookTrack.Core.Entitites;

namespace BookTrack.Core.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetById(int userId);
    Task<bool> Exists(int userId);
}