using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SoundScape.Controllers
{
    [Authorize] 
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jwtId = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var username = User.Identity?.Name;


            return Ok(new { userId, jwtId, username });
        }
    }
}