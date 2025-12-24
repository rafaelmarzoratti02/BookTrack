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

    public async Task Add(User user)
    {
         await _dbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetById(int userId)
    {
        return await _dbContext.Users
            .Include(x=> x.Reviews)
            .ThenInclude(x=> x.Book)
            .SingleOrDefaultAsync(x => x.Id == userId && x.IsActive);
    } 
        
    
    public async  Task<bool> Exists(int userId) => await   _dbContext.Users.AnyAsync(x => x.Id == userId && x.IsActive);
    public async Task<User> Login(string email, string password)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}