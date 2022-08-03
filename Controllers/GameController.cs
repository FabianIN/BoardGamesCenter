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
        [Route("Display_Game", Name = "GetGame")]
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
        [Route("Display_All_Games", Name = "GetAllGames")]
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
        [Route("Search_Games_By_Description", Name = "GetAllGamesByDescription")]
        public IActionResult GetAllGamesByDescription(string descriprion)
        {
            var gamesEntities = _gameUnit.Games.Find(a => a.Deleted == false || a.Deleted == null);
            var gameDescriprion = _gameUnit.Games.Find(a => a.Description == descriprion);
            if (gamesEntities == null)
            {
                return NotFound();
            }

            if (gameDescriprion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GameDTO>>(gameDescriprion));
        }

        [HttpGet, Authorize]
        [Route("Display_Game_Details", Name = "GetGameDetails")]
        public IActionResult GetGameDetails(Guid id)
        {
            var gameEntity = _gameUnit.Games.GetGameDetails(id);
            if(gameEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDTO>(gameEntity));
        }

        [Route("Add_Game", Name = "Add a new game")]
        [HttpPost, Authorize]
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

        [Route("Remove_Game", Name = "Remove an existing game")]
        [HttpDelete, Authorize]
        public IActionResult RemoveGame(Guid id)
        {
            var gameEntity = _gameUnit.Games.GetGameDetails(id);
            if (gameEntity == null)
            {
                return NotFound();
            }

            gameEntity.Deleted = true;
            _gameUnit.Games.Remove(gameEntity);
            _gameUnit.Complete();
            return Ok(gameEntity.Title +" game was deleted.");
        }
        #endregion Games
    }
}
