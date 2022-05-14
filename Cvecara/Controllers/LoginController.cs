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
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "applciation/xml")]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;
        private CvecaraContext context;
        private readonly IKupacRepository kupacRepository;
        public LoginController(IConfiguration config, CvecaraContext context, IKupacRepository kupacRepository)
        {
            this.config = config;
            this.context = context;
            this.kupacRepository = kupacRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] KupacLogin kupacLogin)
        {
            var user = Authenticate(kupacLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(Kupac kupac)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, kupac.korisnickoImeKupca),
                new Claim(ClaimTypes.Email, kupac.emailKupca),
                new Claim(ClaimTypes.GivenName, kupac.imeKupca),
                new Claim(ClaimTypes.Surname, kupac.prezimeKupca),
                new Claim(ClaimTypes.Role, kupac.uloga)
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(45),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Kupac Authenticate(KupacLogin kupacLogin)
        {
            var kupci = kupacRepository.GetAllKupci();

            if (kupci != null) 
            {
                foreach (Kupac k in kupci)
                {
                    if (k.korisnickoImeKupca.ToLower() == kupacLogin.korisnickoIme.ToLower() && k.lozinkaKupca.ToLower() == kupacLogin.lozinka.ToLower())
                    {
                        return k;
                    }
                }
            }

            return null;
        }
    }
}
