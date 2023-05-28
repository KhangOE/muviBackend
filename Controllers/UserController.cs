using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using movie.Data;
using movie.Dto;
using movie.Interfaces;
using movie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private DataContext _dataContext;
        private IConfiguration _config;
        public UserController(IUserRepository userRepository, IMapper mapper,DataContext dataContext,IConfiguration configuration)

        {
            _dataContext = dataContext;
            _userRepository = userRepository;
            _mapper = mapper;
            _config = configuration;

        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUser() {
            var t = User.Claims.ToList();
            string name = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _userRepository.GetUsers().SingleOrDefault(u => u.Name == name);
            var mappUser = _mapper.Map<UserViewDto>(user);
            return Ok(mappUser);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsers(int id) {
        var user = _userRepository.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserRegisterDto userInput)
        {
            if (_dataContext.Users.Any(x => x.UserName == userInput.UserName)) return BadRequest("userName existed");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userInput.Password);

            var user = new User();

            user.UserName = userInput.UserName;
            user.Password = passwordHash;
            user.Name = userInput.Name;
            user.Email = userInput.Email;
            user.Role = false;

            _userRepository.CreateUser(user);
           

            var token = CreateToken(user);
         
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
               _config["Jwt:Secret"]));

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
