using Barber_shops.ApiRespones;
using Barber_shops.CloudinaryServies;
using Barber_shops.DTOs.User;
using Barber_shops.Main;
using Barber_shops.Models;
using Barber_shops.Servies.AuthService;
using Barber_shops.Servies.Emailservies;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Barber_shops.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServies _authServies;
        private readonly MainDbContext _mainDbContext;
        private readonly ICloudinaryServices _cloudinaryServices;
        private readonly IEmailSerives _emailServies;
        public AuthController(IAuthServies authServies, IEmailSerives emailServies, MainDbContext mainDbContext, ICloudinaryServices cloudinaryServices)
        {

            _authServies = authServies;
            _emailServies = emailServies;
            _mainDbContext = mainDbContext;
            _cloudinaryServices = cloudinaryServices;
        }



        [HttpPost("SendOtp")]

        public async Task<IActionResult> sendotp(string email)
        {
            bool isotpset = await _emailServies.sendOtp(email);
            return Ok(isotpset);
        }





        [HttpPost("signup")]
        public async Task<IActionResult> signup([FromForm] UserDto user)
        {
            try
            {
                var res = await _authServies.signup(user);
                //if (!res)
                //{
                //    var r = new ApiRespones<string>(409, "user already exict");
                //    return Conflict(r);
                //}

                return Ok(new ApiRespones<string> (200, res));
            }
            catch (Exception ex)
            {
                var r=new ApiRespones<string>(500,"sewrver error",null,ex.Message);
                return  StatusCode(500,r);
            }
        }




        [HttpPost("addimage")]

        public async Task<IActionResult> addimage([FromForm] int id,IFormFile image)
        {
            try
            {
                string ImageUrl = await _cloudinaryServices.UploadDocumentAsync(image);

                var img = new imageupload
                {

                    url = ImageUrl
                };

                await _mainDbContext.imageuploads.AddAsync(img);
                await _mainDbContext.SaveChangesAsync();
                return Ok(img);
            }
            catch (Exception ex)
            {
                //var r = new ApiRespones<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500,ex.InnerException.Message);
            }
        }

    }
}
