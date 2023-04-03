using Shared.Dtos;

namespace HttpClients.Interfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
}