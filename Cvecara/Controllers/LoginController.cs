using Cvecara.Entities;
using Cvecara.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cvecara.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json", "applciation/xml")]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;
        private CvecaraContext context;
        private readonly IUserRepository userRepository;
        public LoginController(IConfiguration config, CvecaraContext context, IUserRepository userRepository)
        {
            this.config = config;
            this.context = context;
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost ("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                var id = user.userID;
                //AuthenticatedResponse authenticatedResponse = new AuthenticatedResponse { Token = token };
                return Ok(new AuthenticatedResponse { Token = token, id = id });

              //  return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.korisnickoImeUser),
            //    new Claim(ClaimTypes.Email, user.emailUser),
            //    new Claim(ClaimTypes.GivenName, user.imeUser),
            //    new Claim(ClaimTypes.Surname, user.prezimeUser),
            //    new Claim(ClaimTypes.Role, user.uloga)
            //};

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.korisnickoImeUser),
                new Claim(ClaimTypes.Email, user.emailUser),
                new Claim(ClaimTypes.GivenName, user.imeUser),
                new Claim(ClaimTypes.Surname, user.prezimeUser),
                new Claim(ClaimTypes.Role, user.uloga)
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Audience"],
              claims: claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //var tokeOptions = new JwtSecurityToken(
            //    issuer: "https://localhost:44379",
            //    audience: "https://localhost:44379",
            //    claims: new List<Claim>(),
            //    expires: DateTime.Now.AddMinutes(5),
            //    signingCredentials: signinCredentials
            //);

            //var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);



            //return tokenString;

            //return Ok(new AuthenticatedResponse { Token = tokenString });


        }

        private User Authenticate(UserLogin userLogin)
        {
            var kupci = userRepository.GetAllUsers();

            if (kupci != null) 
            {
                foreach (User k in kupci)
                {
                    if (k.korisnickoImeUser.ToLower() == userLogin.korisnickoIme.ToLower() && k.lozinkaUser.ToLower() == userLogin.lozinka.ToLower())
                    {
                        return k;
                    }
                }
            }

            return null;
        }
    }
}
