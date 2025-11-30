using BookTrack.Application.Services;
using BookTrack.Shared.InputModels.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace BookTrack.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByBookId(int bookId)
    {
        var books = await _reviewService.GetAllByBookId(bookId);
        return Ok(books);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _reviewService.GetById(id);
        return Ok(book);
    }

    
    [HttpPost]
    public async Task<IActionResult> Post(CreateReviewInputModel model)
    {
        var book = await _reviewService.Insert(model);
        return CreatedAtAction(nameof(GetById), new { id = book }, model);
    }
}