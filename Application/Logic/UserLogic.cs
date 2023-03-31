using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<User> CreateAsync(User user)
    {
        User? existing = await _userDao.GetByUsernameAsync(user.Username);
        if (existing != null)
        {
            throw new Exception("Username already taken");
        }
//        ValidateData(user);
        User toCreate = new User()
        {
            Email = user.Email,
            Password = user.Password,
            Username = user.Username
        };
        User created = await _userDao.CreateAsync(toCreate);
        return created;
    }

    public static void ValidateData(User user)
    {
        if (user.Username.Length < 3)
            throw new Exception("Username must be at Least 3 characters!");
        if (user.Username.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}