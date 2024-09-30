using ECommerceAPI.Model;
using ECommerceAPI.Model.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationSignInManager _AppSignInManager;
        public AuthController(IConfiguration configuration,ApplicationSignInManager appSignInManager)
        {
            _configuration = configuration;
            _AppSignInManager = appSignInManager;
        }
        [HttpPost]
        public async Task<IActionResult> GenerateToken(UserLogin userLogin)
        {
            var result= await _AppSignInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password,isPersistent:false,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken();
                return Ok(new { token });
            }
            //if(userLogin.UserName=="Avneet" &&  userLogin.Password=="Avneet@1990")
            //{
            //    var token= GenerateJwtToken();
            //    return Ok(new { token });
            //}
            return Unauthorized();
        }
        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
