using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> GetAsync(string username);
    Task DeleteAsync(string username);
}