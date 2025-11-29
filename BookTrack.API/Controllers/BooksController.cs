using Microsoft.AspNetCore.Mvc;

namespace BookTrack.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}