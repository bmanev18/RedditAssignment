using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

<<<<<<< Updated upstream
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(User user)
=======

    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetAsync([FromRoute] string username)
>>>>>>> Stashed changes
    {
        try
        {
            User existing = await _userLogic.GetAsync(username);
            return Ok(existing);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{username}")]
    public async Task<ActionResult<User>> DeleteAsync([FromRoute] string username)
    {
        try
        {
            await _userLogic.DeleteAsync(username);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}