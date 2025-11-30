using BookTrack.Core.Entitites;
using BookTrack.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    
    private readonly BookTrackDbContext _dbContext;

    public UserRepository(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Insert(User user)
    {
        await _dbContext.Users.AddAsync(user);
        return user.Id;
    }

    public async  Task<User?> GetById(int userId) => await  _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId && x.IsActive);
    
    public async  Task<bool> Exists(int userId) => await   _dbContext.Users.AnyAsync(x => x.Id == userId && x.IsActive);
}