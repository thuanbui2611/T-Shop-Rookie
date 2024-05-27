﻿using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Repository.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<UserAuthenResponseModel> Login(UserAuthenRequestModel user);
        Task<UserResponseModel> Register(UserCreationResquestModel user);
    }
}