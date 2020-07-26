using AutoMapper;
using BusinessLayerAbstraction.CDBAbs;
using BusinessLayerAbstraction.UserAbs;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingAppBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICosmosDbService _cosmosDbService;

        #region Constructor

        public UserController(IUserRepository repository, IMapper mapper, ICosmosDbService cosmosDbService)
        {
            _repo = repository;
            _mapper = mapper;
            _cosmosDbService = cosmosDbService;
        }

        #endregion

        #region Action Method

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // var users = await _repo.GetUsers();
            var users = await _cosmosDbService.GetUsers();

            //var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);

            return Ok(userToReturn);
        }
        #endregion
    }
}