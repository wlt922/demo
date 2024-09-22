using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        public TokenController(IConfiguration configuration)
        {
            this._config = configuration;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            if (user.UserName == "admin" && user.Password == "123")
            {
                var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
                var signingCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
                var claims = new[] {
                new Claim(ClaimTypes.Name,user.UserName)
                };
                var tokenOptions = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials

                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = tokenString });


            }
            return Unauthorized();
        }

        public class UserModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
