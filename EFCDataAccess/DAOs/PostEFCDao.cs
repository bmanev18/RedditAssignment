using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;

namespace EFCDataAccess.DAOs;

public class PostEFCDao : IPostDao
{
    private readonly PostContext context;

    public PostEFCDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto)
    {
        IQueryable<Post> query = context.posts.Include(post => post.Owner).AsQueryable();
        if (!string.IsNullOrEmpty(dto.username))
        {
            query = query.Where(post => post.Owner.Username.ToLower().Equals(dto.username.ToLower()));
        }

        if (!string.IsNullOrEmpty(dto.title))
        {
            query = query.Where(post => post.Title.ToLower().Contains(dto.title.ToLower()));
        }

        List<Post> posts = await query.ToListAsync();
        return posts;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post = await context.posts
            .Include(p => p.Owner)
            .SingleOrDefaultAsync(p => p.Id == id);
        return post;
    }
}