using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ScripDraft.WebApi.Models;
using ScripDraft.Data;
using ScripDraft.Data.Entities;

namespace scripdraft.webapi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserRepository _repository = null;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IRepository<User> repository, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _repository = repository as UserRepository;
            _logger = logger;

            if(_repository.Database is null)
            {
                _repository.Database = SDDatabase.GetDatabase(configuration);
            }
        }
        
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]UserModel user)
        {
            // var client = new MongoClient(settings.ConnectionString);
            // Database = client.GetDatabase(settings.DatabaseName);

            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            

            if (user.Username == "testuser1" && user.Password == "testuser1")
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

        private string GenerateToken(UserModel user)
        {
            string token = string.Empty;



            return token;
        }
        
        [HttpPost, Route("signup")]
        public IActionResult Signup([FromBody]UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            bool isUsernameValid = ValidateUserByUsername(user.Username);

            if(isUsernameValid)
            {
                return BadRequest(string.Format("User with username '{0}' already exists.", user.Username));
            }
            else
            {
                ScripDraft.Data.Entities.User userEntity = UserModel.CreateEntity(user);

                var result = _repository.UpsertAsync(userEntity);
            }

            return Ok();
        }

        private bool ValidateUserByUsername(string username)
        {
            bool isValid = true;

            User user = _repository.LoadByUsernameAsync(username).Result;

            isValid = user is null is false;

            return isValid;
        }

        [HttpGet, Route("validate/username")]
        public IActionResult ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be empty");
            }

            bool isValid = ValidateUserByUsername(username);

            return Ok(isValid);
        }
    }
}
