using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerAbstraction.Auth;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;

namespace DatingAppBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        #region Constructor

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        #endregion


        /// <summary>
        /// Action method for user registration
        /// </summary>
        /// <param name="userForRegistrationDto"></param>
        /// <param name="authRepository"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistrationDto, [FromServices] IAuthRepository authRepository)
        {

            if (await authRepository.UserExists(userForRegistrationDto.username.ToLower()))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                Username = userForRegistrationDto.username
            };

            var createdUser = await authRepository.Register(userToCreate, userForRegistrationDto.password);

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto, [FromServices] IAuthRepository authRepository)
        {
            throw new Exception("Test");
            var userFromRepo = await authRepository.Login(userForLoginDto.username.ToLower(), userForLoginDto.password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            #region JWT Auth

            // Creating the cliams to use

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //

            // Generating the key for JWT token

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_config.GetSection("AppSettings:Token").Value));

            //

            // Generating credentials

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //

            // Using a token Descriptor for token generating token

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //

            // Initializing token handler and generating the token

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
            #endregion
        }
    }
}