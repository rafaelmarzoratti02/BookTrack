using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels;
using BookTrack.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Application.Services;

public class BookService : IBookService
{
    
    private readonly BookTrackDbContext _dbContext;

    public BookService(BookTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BookItemViewModel>> GetAll()
    {
        var books = await _dbContext.Books.ToListAsync();
        var model = books.Select(x => BookItemViewModel.FromEntity(x)).ToList();

        return model;
    }

    public async Task<BookViewModel> GetById(int id)
    {
        var book =  await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        
        if(book is null)
            throw new NotFoundException();
        
        var model = BookViewModel.FromEntity(book);
        
        return model;
    }

    public async Task<int> Insert(CreateBookInputModel model)
    {
        var book = model.ToEntity();
        
       await _dbContext.Books.AddAsync(book);
       await _dbContext.SaveChangesAsync();
       
       return book.Id;
    }

    public async Task Update(UpdateBookInputModel model)
    {
        var book  = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == model.IdBook);
        book.Update(model.Title, model.Description,model.YearOfPublication);
        
        _dbContext.Books.Update(book); 
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task Delete(int id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        book.SetAsDeleted();
        
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
    }
}