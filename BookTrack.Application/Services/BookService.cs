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
    
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<List<BookItemViewModel>> GetAll()
    {
        var books = await _bookRepository.GetAll();
        var model = books.Select(x => BookItemViewModel.FromEntity(x)).ToList();

        return model;
    }

    public async Task<BookViewModel> GetById(int id)
    {
        var book =  await  _bookRepository.GetById(id);
        
        if(book is null)
            throw new NotFoundException();
        
        var model = BookViewModel.FromEntity(book);
        
        return model;
    }

    public async Task<int> Insert(CreateBookInputModel model)
    {
        var isbnExists = await _bookRepository.IsbnExists(model.ISBN);
        if(isbnExists)
            throw new IsbnAlreadyExistsException();
        
        var book = model.ToEntity();
        await _bookRepository.Add(book);
        await _unitOfWork.SaveChanges();
       
        return book.Id;
    }

    public async Task Update(UpdateBookInputModel model)
    {
        var book = await _bookRepository.GetById(model.IdBook);
        
        if(book is null)
            throw new NotFoundException();
        
        book.Update(model.Title, model.Description,model.YearOfPublication);
        
        await _unitOfWork.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var book = await _bookRepository.GetById(id);
        if(book is null)
            throw new NotFoundException();
        
        book.SetAsDeleted();
        
        await _unitOfWork.SaveChanges();
    }
}