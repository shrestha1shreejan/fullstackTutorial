using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        #region Constructor

        public AuthController(IAuthRepository repository, IConfiguration config, IMapper mapper)
        {
            _repository = repository;
            _configuration = config;
            _mapper = mapper;
        }

        #endregion

        #region ActionMethods

        /// <summary>
        /// Register the User
        /// </summary>
        /// <param name="userForRegistrationDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDTO userForRegistrationDTO)
        {            

            // convert username to lowercase for consistency
            userForRegistrationDTO.Username = userForRegistrationDTO.Username.ToLower();

            // check if user exists
            if (await _repository.UserExists(userForRegistrationDTO.Username))
            {
                return BadRequest("Username already exists");
            }

            // create the user
            var userToCreate = _mapper.Map<User>(userForRegistrationDTO);

            // Register the user
            var createdUser = await _repository.Register(userToCreate, userForRegistrationDTO.Password);

            var userToReturn = _mapper.Map<UserForDetailDto>(createdUser);
            return CreatedAtRoute("GetUser", new { controller = "Users", id = createdUser.Id}, userToReturn);
        }

        /// <summary>
        /// Action method to login user
        /// </summary>
        /// <param name="userForLoginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            // throw new Exception("broken code");
            var userFromRepo = await _repository.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesciprtor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDesciprtor);

            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user 
            });

        }

        #endregion
    }
}