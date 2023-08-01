using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.WebAPI.Models
{
    public class GameStateDto
    {
        public string GameState { get; set; }

        public GameStateDto(string gameState)
        {
            GameState = gameState;
        }
    }
}
