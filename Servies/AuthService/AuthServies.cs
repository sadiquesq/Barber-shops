using AutoMapper;
using Barber_shops.DTOs.User;
using Barber_shops.Main;
using Barber_shops.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Barber_shops.Servies.AuthService
{
    public class AuthServies:IAuthServies
    {


        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public AuthServies(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }

        public async Task<bool> signup(UserDto user)
        {
            try
            {
                var isExist = await _mainDbContext.users.FirstOrDefaultAsync(e => e.Email == user.Email);

                if (isExist != null)
                {
                    return false;
                }

                var haspassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = haspassword;

                var u = _mapper.Map<User>(user);
                _mainDbContext.users.Add(u);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        

    }
}
