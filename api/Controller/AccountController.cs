using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.AccountDto;
using api.Interfaces;
using api.Model;
using api.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;


        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
  
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
        {
            try{
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName);
                if(user == null)
                {
                    return Unauthorized("Invalid Email!");
                }
                var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if(!result)
                {
                    return Unauthorized("Invalid Email/Password");
                }
                return Ok(
                    new NewUserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDTO)
        {
            try{
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                    Password = registerDTO.Password,
                    PasswordConfirmation = registerDTO.PasswordConfirmation
                };
                
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if(result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else{
                    return StatusCode(500, result.Errors);
                }
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}