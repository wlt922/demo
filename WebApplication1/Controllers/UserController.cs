using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Net.Mime;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        { 
            this._userService = userService; 
        }

        [HttpGet("getmsg")]
        public IActionResult GetMsg()
        {
            var result = _userService.hello();
            return Ok(result);
        }
        [HttpGet("getuser")]
        public IActionResult getUser()
        {
            var result = _userService.GetUsers();
            
            return Ok(result);
        }
        [HttpGet("getuser/{name}/{password}")]
        public IActionResult getUser(string name,string password)
        {
            var result = _userService.GetUser(name,password);
            return Ok(result);
        }
        [HttpPost("uploadfile")]
        public IActionResult UploadFile(IFormFile file)
        {
            return Ok(new { success = true, message = "File uploaded successfully" });
        }

        
    }
}
