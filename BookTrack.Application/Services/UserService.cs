using BookTrack.Core.Entitites;
using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Users;
using BookTrack.Shared.ViewModels.Users;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Application.Services;

public class UserService : IUserService
{
    private readonly BookTrackDbContext _dbContext;

    public UserService(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Insert(CreateUserInputModel model)
    {
        var user = model.ToEntity();
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        
        return user.Id;
    }

    public async Task<UserViewModel> GetById(int userId)
    {
        var user =  await _dbContext.Users
                .Include(x => x.Reviews)
                .ThenInclude(x=> x.Book)
                .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive == true)
;

        if (user is null)
            throw new NotFoundException();
        
        var model =  UserViewModel.FromEntity(user);
        
        return model;
    }

    public async  Task Update(UpdateUserInputModel model)
    {
        var user  = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == model.IdUser);
        
        if(user is null)
            throw new NotFoundException();
        
        user.Update(model.Name);
        
        _dbContext.Users.Update(user); 
        await _dbContext.SaveChangesAsync();
    }

    public async  Task Delete(int userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if(user is null)
            throw new NotFoundException();
        
        user.SetAsDeleted();
        
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}