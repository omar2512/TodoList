using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Infrastrucutre.DataBaseContext;
using TodoList.Infrastrucutre.Dto;
using TodoList.Infrastrucutre.JwtService;

namespace TodoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        public AccountController(JwtService jwtService, UserManager<User> userManager, SignInManager<User> singInManager)
        {
            _jwtService = jwtService ?? throw new NullReferenceException(nameof(jwtService));
            _userManager = userManager ?? throw new NullReferenceException(nameof(userManager));
            _singInManager = singInManager ?? throw new NullReferenceException(nameof(singInManager));
        }


        [HttpPost("logIn")]

        public async Task<ActionResult<UserDto>> Login(LoginDto userDto)
        {

            var user = await _userManager.FindByEmailAsync(userDto.Email!);
            if (user is null) return Unauthorized("Invalid Email or Password");
            if (user.EmailConfirmed == false) return Unauthorized("Email is not confirmed");
            var result = await _singInManager.CheckPasswordSignInAsync(user, userDto.PassWord!, false);
            if (!result.Succeeded) return Unauthorized("Invalid UserName or PassWord");
            return CreateApplicationUserDto(user);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await CheckEmailExistAync(dto.Email!)) return BadRequest($"An existing account email address {dto.Email}, please use another email address");
            var userToAdd = new User
            {
                Email = dto.Email!.ToLower(),
                EmailConfirmed = true,
                FirstName = dto.FirstName!.ToLower(),
                UserName = dto.Email!.ToLower(),
                SecondName = dto.LastName!.ToLower()
            };
            var result = await _userManager.CreateAsync(userToAdd, dto.Password!);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok(new JsonResult(new { title = "create account", message = "your account has been created ,you can login" }));
            // return Ok($"account with email {dto.Email} has been created succefully");
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            string email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Unauthorized("faild to refresh token");
            return CreateApplicationUserDto(user!);
        }
        #region check email exist
        private async Task<bool> CheckEmailExistAync(string Email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == Email);
        }
        #endregion
        #region private helper method
        private UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto
            {
                FirtName = user.FirstName,
                SecondName = user.SecondName,
                Jwt = _jwtService.GenerateJwt(user)
            };
        }
        #endregion
    }
}
