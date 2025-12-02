using BookTrack.Core.Exceptions;
using BookTrack.Core.Repositories;
using BookTrack.Infra.Persistence;
using BookTrack.Shared.Exceptions;
using BookTrack.Shared.InputModels;
using BookTrack.Shared.InputModels.Books;
using BookTrack.Shared.ViewModels;
using BookTrack.Shared.ViewModels.Books;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Application.Services;

public class BookService : IBookService
{
    
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<List<BookItemViewModel>> GetAll()
    {
        var books = await _unitOfWork.Books.GetAll();
        var model = books.Select(x => BookItemViewModel.FromEntity(x)).ToList();

        return model;
    }

    public async Task<BookViewModel> GetById(int id)
    {
        var book =  await _unitOfWork.Books.GetById(id);
        
        if(book is null)
            throw new NotFoundException();
        
        var model = BookViewModel.FromEntity(book);
        
        return model;
    }

    public async Task<int> Insert(CreateBookInputModel model)
    {
        var isbnExists = await _unitOfWork.Books.IsbnExists(model.ISBN);
        if(isbnExists)
            throw new IsbnAlreadyExistsException();
        
        var book = model.ToEntity();
        await _unitOfWork.Books.Add(book);
        await _unitOfWork.CompleteAsync();
       
        return book.Id;
    }

    public async Task Update(UpdateBookInputModel model)
    {
        var book = await _unitOfWork.Books.GetById(model.IdBook);
        
        if(book is null)
            throw new NotFoundException();
        
        book.Update(model.Title, model.Description,model.YearOfPublication);
        
        await _unitOfWork.CompleteAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _unitOfWork.Books.GetById(id);
        if(book is null)
            throw new NotFoundException();
        
        book.SetAsDeleted();
        
        await _unitOfWork.CompleteAsync();
    }
}