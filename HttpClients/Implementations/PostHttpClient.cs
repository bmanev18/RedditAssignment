using System.Net.Http.Json;
using HttpClients.Interfaces;
using Shared.Dtos;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient client)
    {
        _client = client;
    }


    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/post", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}