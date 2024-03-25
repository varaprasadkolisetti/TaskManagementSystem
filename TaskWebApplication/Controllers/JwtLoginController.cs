using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskWebApplication.Models;

namespace TaskWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public JwtLoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Login loginRequest)
        {
           if (loginRequest.Username== "varaprasad" && loginRequest.Password=="12345678")
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddSeconds(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
