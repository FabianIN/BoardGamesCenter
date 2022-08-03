using AutoMapper;
using BoardGamesCenter.Entities;
using BoardGamesCenter.ExternalModels;
using BoardGamesCenter.Services.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesCenter.Controllers
{
    [Route("game")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameUnitOfWork _gameUnit;
        private readonly IMapper _mapper;

        public GameController(IGameUnitOfWork gameUnit, IMapper mapper)
        {
            _gameUnit = gameUnit ?? throw new ArgumentNullException(nameof(gameUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Games
        [HttpGet, Authorize]
        [Route("{id}", Name = "GetGame")]
        public IActionResult GetGame (Guid id)
        {
            var gameEntity = _gameUnit.Games.Get(id);
            if (gameEntity == null)
            {
                return NotFound();
            }  

            return Ok(_mapper.Map<GameDTO>(gameEntity));
        }

        [HttpGet, Authorize]
        [Route("", Name = "GetAllGames")]
        public IActionResult GetAllGames()
        {
            var gamesEntities = _gameUnit.Games.Find(a => a.Deleted == false || a.Deleted == null);
            if (gamesEntities == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<GameDTO>>(gamesEntities));
        }

        [HttpGet, Authorize]
        [Route("details/{id}", Name = "GetGameDetails")]
        public IActionResult GetGameDetails(Guid id)
        {
            var gameEntity = _gameUnit.Games.GetGameDetails(id);
            if(gameEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDTO>(gameEntity));
        }

        [Route("add", Name = "Add a new game")]
        [HttpGet, Authorize]
        public IActionResult AddGame([FromBody] GameDTO game)
        {
            var gameEntity = _mapper.Map<Game>(game);
            _gameUnit.Games.Add(gameEntity);
            _gameUnit.Complete();
            _gameUnit.Games.Get(gameEntity.Id);

            return CreatedAtRoute("GetGame",
                new { id = gameEntity.Id },
                _mapper.Map<GameDTO>(gameEntity));
        }

        #endregion Games
    }
}
