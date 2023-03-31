using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User>();

    public AuthService()
    {
        loadInitial();
    }


    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUserAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        
        // Todo remove list when file saving is added
        
        users.Add(user);
        return Task.CompletedTask;
    }

    private void loadInitial()
    {
        users.Add(new User
        {
            Email = "trmo@via.dk",
            Password = "onetwo3FOUR",
            Username = "trmo",
        });

        users.Add(new User
        {
            Email = "jakob@gmail.com",
            Password = "password",
            Username = "jknr",
        });
    }
}