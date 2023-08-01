using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.CodeLibrary
{
    public class GameService
    {
        private readonly int rows = 14;
        private readonly int columns = 36;
        private readonly AnimalFactory _animalFactory = new AnimalFactory();
        private string _gameState = string.Empty;
        private Game _game = null!;
        private IEnumerator<GeneratedGrid> _gameStateEnumerator = null!;
        private GridStringifier _stringifier = GridStringifier.AddCellChar();
        private GeneratedGrid _grid = null!;

        private string StringifyGameState(GeneratedGrid gameState)
        {
            return _stringifier.Stringify(gameState);
        }

        public void StartGame()
        {
            var rules = GameRules.Instance;
            _grid = GeneratedGrid.CreateFromConsoleWidth(rows, columns);

            _game = Game.Create(_grid, rules);
            _gameStateEnumerator = _game.GetEnumerator();
        }

        public void GenerateNextGameState()
        {
            _gameStateEnumerator.MoveNext();

            var currentGameState = _gameStateEnumerator.Current;

            _gameState = StringifyGameState(currentGameState);
        }

        public string GetGameState()
        {
            return _gameState;
        }

        public string GetAnimalInfo()
        {
            return _stringifier.DisplayAnimalInfo();
        }

        public List<string> GetAnimalsList()
        {
            return _stringifier.GetDisplayedAnimalsList();
        }

        public void AddAnimal(char key)
        {
            UserInputService.HandleUserAnimalInput(key, _gameStateEnumerator.Current, _stringifier.GetCurrentAnimalList(), _animalFactory);
        }
    }
}
