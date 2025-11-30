using BookTrack.Application.Services;
using BookTrack.Application.Validators;
using BookTrack.Shared.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace BookTrack.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
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
    public async Task<IActionResult> Post(CreateBookInputModel model)
    {
        var book = await _bookService.Insert(model);
        return CreatedAtAction(nameof(GetById), new { id = book }, model);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpdateBookInputModel model)
    {
        await _bookService.Update(model);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {   
        await _bookService.Delete(id);
        return NoContent();
    }
}