using Microsoft.AspNetCore.Mvc;
using Savanna.CodeLibrary;
using Savanna.WebAPI.Models;

namespace Savanna.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("gameState", Name = "GetGameStateConfiguration")]
        public ActionResult<GameStateDto> GetGameStateConfiguration()
        {
            _gameService.GenerateNextGameState();

            var gameState = _gameService.GetGameState();

            if (string.IsNullOrEmpty(gameState))
            {
                return NoContent();
            }

            var gameStateDto = new GameStateDto(gameState);

            return Ok(gameStateDto);
        }

        [HttpGet("animalInfo", Name = "GetAnimalInfo")]
        public ActionResult<string> GetAnimalInfo()
        {
            var animalInfo = _gameService.GetAnimalInfo();

            if (string.IsNullOrEmpty(animalInfo))
            {
                return NoContent();
            }

            return Ok(animalInfo);
        } 

        [HttpGet("animalList", Name = "GetAnimalsList")]
        public ActionResult<List<string>> GetAnimalsList()
        {
            var animalsList = _gameService.GetAnimalsList();

            if (animalsList == null)
            {
                return NoContent();
            }

            var animalsListDto = new AnimalsListDto(animalsList);

            return Ok(animalsListDto);
        }

        [HttpPost("startGame", Name = "StartGame")]
        public IActionResult StartGame()
        {
            _gameService.StartGame();

            return Ok();
        }

        [HttpPost("addAnimal/{key}", Name = "AddAnimal")]
        public IActionResult AddAnimal(char key)
        {
            _gameService.AddAnimal(key);
            var added = true;

            if (added)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}