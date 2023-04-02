using Application.DaoInterfaces;
using Shared.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext _context;

    public UserFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        Task<User?> existing = GetByUsernameAsync(user.Username);
        if (existing.Result!=null)
        {
            throw new Exception($"{user.Username} already exists in the System");
        }
        _context.Users.Add(user);
        _context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing =
            _context.Users.FirstOrDefault(u => u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(string username)
    {
        Task<User?> existing = GetByUsernameAsync(username);
        if (existing.Result == null)
        {
            throw new Exception($"{username} doesn't exist");
        }

        _context.Users.Remove(existing.Result);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}