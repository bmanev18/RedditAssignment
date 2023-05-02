using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EFCDataAccess;

public class PostContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Post> posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EFCDataAccess/Post.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Username);
    }
}