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

    public async Task DeleteAsync(string username)
    {
        User? existing = _context.Users.FirstOrDefault(user => user.Username.Equals(username));
        if (existing == null)
        {
            throw new Exception($"{username} doesn't exist");
        }

        _context.Users.Remove(existing);
        _context.SaveChanges();
    }
}