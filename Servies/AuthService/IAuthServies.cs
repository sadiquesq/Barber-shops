﻿using Barber_shops.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Barber_shops.Servies.AuthService
{
    public interface IAuthServies
    {

        Task<bool> signup([FromForm] UserDto user);

    }
}