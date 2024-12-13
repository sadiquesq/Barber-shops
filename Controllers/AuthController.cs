using Barber_shops.ApiRespones;
using Barber_shops.DTOs.User;
using Barber_shops.Servies.AuthService;
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

        public AuthController(IAuthServies authServies)
        {
            _authServies = authServies;
        }



        [HttpPost("signup")]
        public async Task<IActionResult> signup([FromForm] UserDto user)
        {
            try
            {
                var res = await _authServies.signup(user);
                if (!res)
                {
                    var r = new ApiRespones<string>(409, "user already exict");
                    return Conflict(r);
                }

                return Ok(new ApiRespones<bool>(200, "successfully registered", res));
            }
            catch (Exception ex)
            {
                var r=new ApiRespones<string>(500,"sewrver error",null,ex.Message);
                return  StatusCode(500,r);
            }
        }
    }
}
