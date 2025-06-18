using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Models.DTOs;
using NzWalks.Repositories;

namespace NzWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepo tokenRepo;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepo tokenRepo)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add role to User
                if (registerRequestDto.Password != null && registerRequestDto.Roles.Any())
                {
                    await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered! Login") ;
                    }
                }

            }
           
            
            return BadRequest("Something Went Wrong");
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginrequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
              var checkpassword =   await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if(checkpassword)
                { 
                    //Get Role for User
                    var role = await userManager.GetRolesAsync(user);

                    if(role != null)
                    {
                        //Create token

                     var jwtToken =    tokenRepo.CreateToken(user, role.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                     return Ok(response);

                    }
                }
            }

            return BadRequest("Username or password is incorrect");
        }
    }
}
