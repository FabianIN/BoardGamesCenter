using AutoMapper;
using BoardGamesCenter.Entities;
using BoardGamesCenter.ExternalModels;
using BoardGamesCenter.Services.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpGet, Authorize]
        [Route("Display_User", Name = "GetUser")]
        public IActionResult GetUser(Guid id)
        {
            var userEntity = _userUnit.Users.Get(id);
            if (userEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTO>(userEntity));
        }

        [HttpGet, Authorize]
        [Route("Display_All_Users", Name = "GetAllUser")]
        public IActionResult GetAllUser()
        {
            var userEntities = _userUnit.Users.Find(u => u.Deleted == false || u.Deleted == null);
            if (userEntities == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<UserDTO>>(userEntities));
        }

        [HttpGet, Authorize]
        [Route("Search_Users_By_Name", Name = "GetAllUsersByName")]
        public IActionResult GetAllUsersByName(string name)
        {
            var userEntities = _userUnit.Users.Find(a => a.Deleted == false || a.Deleted == null);
            var userName = _userUnit.Users.Find(a => a.FirstName == name);
            if (userEntities == null)
            {
                return NotFound();
            }

            if (userName == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<UserDTO>>(userName));
        }

        [Route("Sign_Up", Name = "Register a new account")]
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


        [Route("Sign_In")]
        [HttpPost]

        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request.");
            }

            var foundUsers = _userUnit.Users.Find(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password) && (u.Deleted == false || u.Deleted == null));

            if (foundUsers.Count() == 1)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7049",
                    audience: "https://localhost:7049",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });

            }

            else
            {
                return Unauthorized();
            }
        }

        [Route("Remove_User", Name = "Remove an existing user")]
        [HttpDelete, Authorize]
        public IActionResult RemoveGame(Guid id)
        {
            var userEntity = _userUnit.Users.Get(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            userEntity.Deleted = true;
            _userUnit.Users.Remove(userEntity);
            _userUnit.Complete();
            return Ok(userEntity.FirstName + " was deleted.");
        }
    }
}
