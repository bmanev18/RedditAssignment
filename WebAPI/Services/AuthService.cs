using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using FileData.DAOs;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao _userDao;

    public AuthService(IUserDao dao)
    {
        _userDao = dao;
        //loadInitial();
    }


    public async Task<User>  ValidateUserAsync(string username, string password)
    { 
        User? existingUser = await _userDao.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
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
      
        _userDao.CreateAsync(user);
        return Task.CompletedTask;
    }

    private void loadInitial()
    {
        _userDao.CreateAsync(new User
        {
            Email = "trmo@via.dk",
            Password = "onetwo3FOUR",
            Username = "trmo",
        });

        _userDao.CreateAsync(new User
        {
            Email = "jakob@gmail.com",
            Password = "password",
            Username = "jknr",
        });
    }
}