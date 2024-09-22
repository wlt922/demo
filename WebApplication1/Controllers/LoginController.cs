using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Get(
    [FromHeader(Name = "X-Authorization")] string authorizationHeader,
    [FromHeader(Name = "X-Request-ID")] string requestId)
        {
            authorizationHeader = authorizationHeader ?? string.Empty;
            requestId = requestId ?? string.Empty;
            var token= authorizationHeader.Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:key"]);
        
            tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters { 
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=new SymmetricSecurityKey(key),
                ValidateIssuer=false,
                ValidateAudience=false,
                ClockSkew=TimeSpan.Zero
            },out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;

            return new JsonResult(jwtToken);
        }
    }
}
