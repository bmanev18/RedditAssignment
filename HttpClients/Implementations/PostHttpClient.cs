using System.Net.Http.Json;

using System.Security.Cryptography;
using System.Text.Json;
using HttpClients.Interfaces;
using Shared.Dtos;
using Shared.Models;
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

    public async Task<ICollection<Post>> getAsync(string? title, string? username)
    {
        string query = ConstructQuery(username, title);
        
        HttpResponseMessage response = await _client.GetAsync("/post"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
           PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> getAsyncByID(int id)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/post/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }
    
    private static string ConstructQuery(string? username, string? title)
    {
        string query = "";
        if (!string.IsNullOrEmpty(username))
        {
            query += $"?username={username}";
        }
        
        if (!string.IsNullOrEmpty(title))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={title}";
        }

        return query;
    }
}