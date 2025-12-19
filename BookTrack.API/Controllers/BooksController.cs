using BookTrack.Application.Commands.BookCommands.AddBook;
using BookTrack.Application.Commands.BookCommands.UpdateBook;
using BookTrack.Application.Services;
using BookTrack.Shared.InputModels.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrack.API.Controllers;

[ApiController]
[Route("api/books")]
[Authorize]
public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IMediator _mediator;


    public BooksController(IBookService bookService, IMediator mediator )
    {
        _bookService = bookService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAll();
        return Ok(books);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetById(id);
        return Ok(book);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(InsertBookCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = command }, command);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpdateBookCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {   
        await _bookService.Delete(id);
        return NoContent();
    }
}