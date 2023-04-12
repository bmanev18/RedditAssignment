using System.Collections;
using System.Net.Mime;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDto dto)
    {
        try
        {
            Post created = await _postLogic.CreateAsync(dto);
            return Created($"/post/{created.Id}", created);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? username, string? title)
    {
        try
        {
            SearchPostParametersDto parametersDto = new(username, title);
            var posts = await _postLogic.getAllAsync(parametersDto);
            return Ok(posts);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    } 
    
    
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetAsync([FromRoute] int id)
    {
        try
        {
            Post post = await _postLogic.getByIdAsync(id);
            return Ok(post);

        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return StatusCode(500, e.Message);
        }
    }
}