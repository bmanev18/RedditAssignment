namespace Shared.Dtos;

public class PostCreationDto
{
    public string owner { get; }
    public string title { get; }
    public string body { get; }

    public PostCreationDto(string owner, string title, string body)
    {
        this.owner = owner;
        this.title = title;
        this.body = body;
    }
}