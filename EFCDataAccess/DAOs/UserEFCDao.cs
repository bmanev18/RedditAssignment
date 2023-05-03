using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EFCDataAccess.DAOs;

public class UserEFCDao : IUserDao
{
    private readonly PostContext context;

    public UserEFCDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> addedUser = await context.users.AddAsync(user);
        await context.SaveChangesAsync();
        return addedUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;

    }

    public async Task DeleteAsync(string username)
    {
        User? existing = await GetByUsernameAsync(username);
        if (existing == null)
        {
            throw new Exception("Such User Does Not Exist");
        }

        context.users.Remove(existing);
        await context.SaveChangesAsync();
    }
}