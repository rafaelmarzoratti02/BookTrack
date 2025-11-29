using BookTrack.Shared.InputModels;
using BookTrack.Shared.ViewModels;

namespace BookTrack.Application.Services;

public class BookService : IBookService
{
    public Task<List<BookItemViewModel>> GetALl()
    {
        throw new NotImplementedException();
    }

    public Task<BookViewModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> Insert(CreateBookInputModel model)
    {
        throw new NotImplementedException();
    }

    public Task Update(UpdateBookInputModel model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}