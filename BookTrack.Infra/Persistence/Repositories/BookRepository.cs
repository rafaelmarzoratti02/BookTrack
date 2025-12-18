using BookTrack.Core.Entitites;
using BookTrack.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Infra.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookTrackDbContext _dbContext;

    public BookRepository(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Book>> GetAll() => await _dbContext.Books.Where(x=> x.IsActive).ToListAsync();
    
    public async Task<Book?> GetById(int id)
    {
        return await _dbContext.Books
            .Include(x=> x.Reviews)
            .SingleOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task<int> Add(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        return book.Id;
    }
    
    public async Task<bool> IsbnExists(string isbn) => await  _dbContext.Books.AnyAsync(x=> x.Isbn == isbn);
    public async Task<bool> Exists(int id) => await _dbContext.Books.AnyAsync(x => x.Id == id && x.IsActive);
}