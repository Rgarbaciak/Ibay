using ClassIbay;
using IbayApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MonProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IbayContext _userContext;

        public SecurityController(IConfiguration config, IbayContext userContext)
        {
            _config = config;
            _userContext = userContext;
        }

        [AllowAnonymous]
        [HttpPost("createToken")]
        public IActionResult CreateToken([FromBody] User user)
        {
            var dbUser = _userContext.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (dbUser != null)
            {
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", dbUser.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, dbUser.Email),
                        new Claim(JwtRegisteredClaimNames.Email, dbUser.Password),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Pseudo", dbUser.Pseudo),
                        new Claim("Role", dbUser.Role),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                // Ajout du cookie
                var options = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    HttpOnly = true,
                    Secure = Request.IsHttps
                };
                var userId = Convert.ToString(dbUser.Id);
                Response.Cookies.Append("jwtToken", jwtToken, options);
                Response.Cookies.Append("role", dbUser.Role, options);
                Response.Cookies.Append("userId", userId, options);
                return Ok(new { access_token = jwtToken });
            }
            return Unauthorized();
        }
    }
}
