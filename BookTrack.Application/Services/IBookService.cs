using BookTrack.Shared.InputModels;
using BookTrack.Shared.InputModels.Books;
using BookTrack.Shared.ViewModels.Books;

namespace BookTrack.Application.Services;

public interface IBookService
{
    Task<List<BookItemViewModel>> GetAll();
    Task<BookViewModel> GetById(int id);
    Task<int> Insert(CreateBookInputModel model);
    Task Update(UpdateBookInputModel model);
    Task Delete(int id);
}