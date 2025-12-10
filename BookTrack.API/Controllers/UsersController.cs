using BookTrack.Application.Services;
using BookTrack.Shared.InputModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace userTrack.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CreateUserInputModel model)
    {
        var user = await _userService.Insert(model);
        return CreatedAtAction(nameof(GetById), new { id = user }, model);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpdateUserInputModel model)
    {
        await _userService.Update(model);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {   
        await _userService.Delete(id);
        return NoContent();
    }
    
    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginInputModel model)
    {   
        var user = await _userService.Login(model);
        return Ok(user);
    }
}