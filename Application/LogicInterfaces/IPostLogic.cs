using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> getAllAsync(SearchPostParametersDto dto);


    Task<Post> getByIdAsync(int id);
}