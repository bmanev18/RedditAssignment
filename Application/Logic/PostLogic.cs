using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;


    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }


    public async Task<Post> CreateAsync(PostCreationDto PostCreationDto)
    {
        User? user = await _userDao.GetByUsernameAsync(PostCreationDto.owner);
        if (user == null)
        {
            throw new Exception("User with that username does not exist");
        }

        ValidatePost(PostCreationDto);
        Post post = new Post(user, PostCreationDto.title, PostCreationDto.body);
        Post created = await _postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> getAllAsync(SearchPostParametersDto dto)
    {
        return _postDao.GetAsync(dto);
    }

    public Task<Post> getById(int id)
    {
        return _postDao.GetByIdAsync(id);
    }


    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.title))
        {
            throw new Exception("Post has to have a title");
        }

        if (dto.body.Length > 1000)
        {
            throw new Exception("Post cannot be longer than 1000 characters");
        }
    }
}