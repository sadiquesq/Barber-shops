using AutoMapper;
using Barber_shops.DTOs.User;
using Barber_shops.Main;
using Barber_shops.Models;
using Barber_shops.Servies.Emailservies;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Barber_shops.Servies.AuthService
{
    public class AuthServies:IAuthServies
    {


        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly IEmailSerives _emailSerives;

        public AuthServies(MainDbContext mainDbContext, IMapper mapper, IEmailSerives emailSerives)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _emailSerives = emailSerives;
        }

        public async Task<string> signup(UserDto user)
        {
            try
            {
                var isExist = await _mainDbContext.users.FirstOrDefaultAsync(e => e.Email == user.Email);

                if (isExist != null)
                {
                    return "user is already exist";
                }

                bool emailverify=  _emailSerives.verifyOtp(user.Email,user.otp);
                if (emailverify)
                {

                    var haspassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.Password = haspassword;

                    var u = _mapper.Map<User>(user);
                    _mainDbContext.users.Add(u);
                    await _mainDbContext.SaveChangesAsync();


                    return "succesfully registered";
                }
                return "wrong otp";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        

    }
}
