using BookTrack.Core.Entitites;
using BookTrack.Shared.InputModels.Users;
using BookTrack.Shared.ViewModels.Users;

namespace BookTrack.Application.Services;

public interface IUserService
{
    Task<int> Insert(CreateUserInputModel user);
    Task<UserViewModel> GetById(int userId);
    Task Update(UpdateUserInputModel user);
    Task Delete(int userId);
    Task<LoginViewModel> Login(LoginInputModel model);
}