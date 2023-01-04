using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using LibraryWithIdentity.Models;

namespace JWTAuthenticationExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly LibraryContext _context;
        //private readonly UserManager<IdentityUser> _userManager;

        private List<UserEntity> appUsers;
        public LoginController(IConfiguration config, LibraryContext context)
        {
            //_userManager= userManger;
            _config = config;
            _context = context;
            appUsers = _context.UserEntities.ToList();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserEntity login)
        {
            IActionResult response = Unauthorized();
            UserEntity user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            else
            {
                 response = Ok("nadarim");
            }
            return response;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserEntity reg)
        {
            IActionResult response = Unauthorized();
            UserEntity user = AuthenticateUser(reg);
            if (user != null)
            {
                response=Ok("mojode");
            }
            else
            {
                _context.UserEntities.Add(reg);
                _context.SaveChanges();
                 response = Ok("sakhte shod");

            }
            return response;
        }
        UserEntity AuthenticateUser(UserEntity loginCredentials)
        {
            UserEntity user = appUsers.SingleOrDefault(x => x.Name == loginCredentials.Name && x.LastName == loginCredentials.LastName);
            return user;
        }
        string GenerateJWTToken(UserEntity userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwtTokenConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               
                new Claim("Name", userInfo.Name.ToString()),
                new Claim("LastName", userInfo.LastName.ToString()),
                new Claim("Id", userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["jwtTokenConfig:Issuer"],
            audience: _config["jwtTokenConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}