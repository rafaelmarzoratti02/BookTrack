using BookTrack.Application.Commands.ReviewsCommands.InsertReview;
using BookTrack.Application.Queries.BookQueries.GetAllBooks;
using BookTrack.Application.Queries.ReviewQueries.GetAllByBookId;
using BookTrack.Application.Services;
using BookTrack.Shared.InputModels.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrack.API.Controllers;

[ApiController]
[Route("api/reviews")]
[Authorize]
public class ReviewsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IReviewService _reviewService;

    public ReviewsController(IMediator mediator, IReviewService reviewService)
    {
        _reviewService = reviewService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByBookId(int bookId)
    {
        var books = await _mediator.Send(new GetAllByBookIdQuery(bookId));
        return Ok(books);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _reviewService.GetById(id);
        return Ok(book);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(InsertReviewCommand command)
    {
        var book = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = book }, command);
    }
}