using Shared.Dtos;
using Shared.Models;

namespace HttpClients.Interfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> getAsync(string? title, string? username);
}