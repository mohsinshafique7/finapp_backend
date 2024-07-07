using finapp_backend.Dtos.Account;
using finapp_backend.Interfaces;
using finapp_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finapp_backend.Controllers;

[Route("api/account]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUsers> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUsers> _signInManager;
    public AccountController(UserManager<AppUsers> userManager, ITokenService tokenService, SignInManager<AppUsers> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = new AppUsers
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email
            };
            var createUser = await _userManager.CreateAsync(appUser, registerModel.Password);
            if (createUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Tokens = _tokenService.createToken(appUser)
                    });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createUser.Errors);
            }


        }
        catch (Exception ex)
        {

            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
        if (user == null)
        {
            return Unauthorized("Invalid username");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid password");
        }
        return Ok(new NewUserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Tokens = _tokenService.createToken(user)
        });
    }

}