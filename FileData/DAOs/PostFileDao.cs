using Application.DaoInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext _context;


    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (_context.Posts.Any())
        {
            id = _context.Posts.Max(p => p.Id);
            id++;
        }

        post.Id = id;
        _context.Posts.Add(post);
        _context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto)
    {
        IEnumerable<Post> result = _context.Posts.AsEnumerable();
        if (!string.IsNullOrEmpty(dto.username))
        {
            result = _context.Posts.Where(post => post.Owner.Username.Equals(dto.username));
        }

        // if (dto.username != null)
        // {
        //     result = result.Where(post => post.Owner.Username.Equals(dto.username));
        // }

        if (!string.IsNullOrEmpty(dto.title))
        {
            result = result.Where(post => post.Title.Contains(dto.title, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
}