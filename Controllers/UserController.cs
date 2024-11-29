using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //user Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                await _userService.RegisterUser(user);
                return Ok(new { message = "User Registered Successfully!!!." });
            }
            catch (Exception excep)
            {
                return BadRequest(excep.Message);
            }
        }


        //Login User
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginReq)
        {
            try
            {
                var token = await _userService.AuthenticateUser(loginReq.Email, loginReq.Password);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


    }
}
