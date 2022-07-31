using AutoMapper;
using BoardGamesCenter.Entities;
using BoardGamesCenter.ExternalModels;
using BoardGamesCenter.Services.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesCenter.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUnitOfWork _userUnit;
        private readonly IMapper _mapper;

        public UserController(IUserUnitOfWork userUnit, IMapper mapper)
        {
            _userUnit = userUnit ?? throw new ArgumentNullException(nameof(userUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("{id}", Name = "GetUser")]
        public IActionResult GetUser(Guid id)
        {
            var userEntity = _userUnit.Users.Get(id);
            if (userEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTO>(userEntity));
        }

        [HttpGet]
        [Route("", Name = "GetAllUser")]
        public IActionResult GetAllUser()
        {
            var userEntities = _userUnit.Users.Find(u => u.Deleted == false || u.Deleted == null);
            if (userEntities == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<UserDTO>>(userEntities));
        }

        [Route("register", Name = "Register a new account")]
        [HttpPost]
        public IActionResult Register([FromBody] UserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            _userUnit.Users.Add(userEntity);

            _userUnit.Complete();

            _userUnit.Users.Get(userEntity.ID);

            return CreatedAtRoute("GetUser",
                new { id = userEntity.ID },
                _mapper.Map<UserDTO>(userEntity));
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request.");
            }

            var foundUser = _userUnit.Users.FindDefault(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password) && (u.Deleted == false || u.Deleted == null));

            if (foundUser != null)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
