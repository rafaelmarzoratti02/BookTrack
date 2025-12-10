using BookTrack.Core.Entitites;
using BookTrack.Core.Repositories;
using BookTrack.Core.Services;
using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels.Users;
using BookTrack.Shared.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookTrack.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public UserService(IUnitOfWork unitOfWork, IAuthService authService,  IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _configuration = configuration;
    }

    public async Task<int> Insert(CreateUserInputModel model)
    {
        var passwordWithSalt = _authService.ComputeSha256Hash(model.Password + _configuration["Jwt:Salt"]);
        var user = model.ToEntity(passwordWithSalt);
        
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

    public async  Task<LoginViewModel> Login(LoginInputModel model)
    {
        var passwordWithSalt = model.Password + _configuration["Jwt:Salt"];
        var password = _authService.ComputeSha256Hash(passwordWithSalt);

        var user = await _unitOfWork.Users.Login(model.Email, password);

        if (user is null)
            throw new NotFoundException();

        var token = _authService.GenerateJWTToken(user.Email, user.Role);

        return new LoginViewModel(user.Email, token);
    }
}