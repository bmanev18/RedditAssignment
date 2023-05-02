using Application.DaoInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace EFCDataAccess.DAOs;

public class PostEFCDao : IPostDao
{
    private readonly PostContext _context;

    public PostEFCDao(PostContext context)
    {
        context = _context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}