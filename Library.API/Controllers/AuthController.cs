using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.DAL;
using Library.API.DTO;
using Library.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly LibContext _context;

        public AuthController(UserManager<User> userManager, IMapper mapper, LibContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult GetAction()
        {
            return Ok("Nice");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLogin login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if(user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var roleStrings = await _userManager.GetRolesAsync(user);
                var role = roleStrings[0];
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

                var signKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("weneedalongersecretkey"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44360",
                    audience: "https://localhost:44360",
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }
    }
}