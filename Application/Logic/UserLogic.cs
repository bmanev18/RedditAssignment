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


    public async Task<User> GetAsync(string username)
    {
        User? existing = await _userDao.GetByUsernameAsync(username);
        if (existing == null)
        {
            throw new Exception("User doesn't exist");
        }

        return existing;
    }

    public async Task DeleteAsync(string username)
    {
        User? existing = await _userDao.GetByUsernameAsync(username);
        if (existing == null)
        {
            throw new Exception($"{username} doesn't exist");
        }

        await _userDao.DeleteAsync(username);
    }
}