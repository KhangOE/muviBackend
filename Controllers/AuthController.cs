using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using movie.Data;
using movie.Dto;
using movie.Interfaces;
using movie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
      
        private IConfiguration _configuration;
        private DataContext _dataContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public AuthController(IConfiguration configuration,DataContext dataContext,IMapper mapper,IUserRepository userRepository) {
            _configuration = configuration;
            _dataContext = dataContext;
            _mapper = mapper;
            _userRepository = userRepository;
        }



        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userInput)
        {
            var user = _mapper.Map<User>(userInput);
            if (_userRepository.Login(user))
            {
               var token = CreateToken(user);
                return Ok(token);
            }


            return BadRequest("userName or password wrong");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "1234 sdf a df asdf s sdf sdf s d d"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

     
    }
}
