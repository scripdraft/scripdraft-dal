using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using scripdraft.webapi.Models;

namespace scripdraft.webapi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            if (user.UserName == "testuser1" && user.Password == "testuser1")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
        
        [HttpPost, Route("signup")]
        public IActionResult Signup([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            return Ok();
        }

        [HttpGet, Route("validate/username")]
        public IActionResult ValidateUsername(string username)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be empty");
            }

            return Ok(isValid);
        }
    }
}
