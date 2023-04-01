namespace Shared.Models;

public class Post
{
 public int Id { get; set; }
 public User Owner { get; }
 public String Title { get; }
 public String Body { get; }

 public Post(User owner, string title, string body)
 {
  Owner = owner;
  Title = title;
  Body = body;
 }
}