using BookTrack.Shared.InputModels;
using BookTrack.Shared.ViewModels;

namespace BookTrack.Application.Services;

public interface IBookService
{
    Task<List<BookItemViewModel>> GetALl();
    Task<BookViewModel> GetById(int id);
    Task<int> Insert(CreateBookInputModel model);
    Task Update(UpdateBookInputModel model);
    Task Delete(int id);
}