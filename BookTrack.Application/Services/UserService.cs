using BookTrack.Core.Entitites;
using BookTrack.Core.Repositories;
using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Users;
using BookTrack.Shared.ViewModels.Users;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Insert(CreateUserInputModel model)
    {
        var user = model.ToEntity();
        
        await _unitOfWork.Users.Add(user);
        await _unitOfWork.CompleteAsync();
        
        return user.Id;
    }

    public async Task<UserViewModel> GetById(int userId)
    {
        var user =  await _unitOfWork.Users.GetById(userId);

        if (user is null)
            throw new NotFoundException();
        
        var model =  UserViewModel.FromEntity(user);
        
        return model;
    }

    public async  Task Update(UpdateUserInputModel model)
    {
        var user  = await  _unitOfWork.Users.GetById(model.IdUser);
        
        if(user is null)
            throw new NotFoundException();
        
        user.Update(model.Name);
        
        await _unitOfWork.CompleteAsync();
    }

    public async  Task Delete(int userId)
    {
        var user = await _unitOfWork.Users.GetById(userId);
        if(user is null)
            throw new NotFoundException();
        
        user.SetAsDeleted();
        
        await _unitOfWork.CompleteAsync();
    }
}