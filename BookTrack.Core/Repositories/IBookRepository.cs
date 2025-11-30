using BookTrack.Core.Entitites;

namespace BookTrack.Core.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAll();
    Task<Book> GetById(int id);
    Task<int> Add(Book book);
    Task<bool> IsbnExists(string isbn);
    Task<bool> Exists(int id);
    
}