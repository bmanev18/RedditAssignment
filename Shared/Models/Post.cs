namespace Shared.Models;

public class Post
{
 public int Id;
 public User Owner;
 public String Title;
 public String Body;


 public Post(User owner, String title, String body)
 {
  Owner = owner;
  Title = title;
  Body = body;
 }
}