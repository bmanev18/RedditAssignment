namespace Shared.Dtos;

public class SearchPostParametersDto
{
    public string? username { get;}
    public string? title { get; }

    public SearchPostParametersDto(string? username, string? title)
    {
        this.username = username;
        this.title = title;
    }
}