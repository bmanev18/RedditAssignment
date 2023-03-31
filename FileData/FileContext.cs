using System.Net.Http.Json;
using System.Text.Json;
using Shared.Models;

namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return _dataContainer!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _dataContainer!.Users;
        }
    }

    private void LoadData()
    {
        if(_dataContainer != null) return; //if the data is already loaded, return
        if (!File.Exists(FilePath)) // check if file exists, if not create a new empty container
        {
            _dataContainer = new()
            {
                Posts = new List<Post>(),
                Users = new List<User>()
            };
            return;
        }

        string content = File.ReadAllText(FilePath); // file exists, read all that
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content); // deserialize the data
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer); //serialize the data into json
        File.WriteAllText(FilePath,serialized); //write all the data into the file
        _dataContainer = null; //empty the data container
    }
}