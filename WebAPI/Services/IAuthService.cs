﻿using Shared.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task RegisterUserAsync(User user);
}