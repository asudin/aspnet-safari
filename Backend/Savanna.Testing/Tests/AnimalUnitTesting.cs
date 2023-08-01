using Savanna.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Configurations.Savanna;
using Savanna.ConsoleApp;
using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary;

namespace AnimalTesting
{
    [TestClass]
    public class AnimalUnitTesting
    {
        private const int _rows = 20;
        private const int _columns = 40; 
        private readonly GeneratedGrid _grid = GeneratedGrid.CreateFromConsoleWidth(_rows, _columns);
        private readonly GameRules _rules = GameRules.Instance;
        private readonly MovingService _movingService = new MovingService();
        private readonly AnimalFactory _animalFactory = new AnimalFactory();

        [TestMethod]
        public void TestUserInput_AddPredator()
        {
            // Arrange
            ConsoleKey addPredatorKey = ConsoleKey.L;
            var predator = new Predator();
            var grid = GeneratedGrid.CreateFromConsoleWidth(_rows, _columns);
            var sortedDictionary = new SortedDictionary<string, List<Animal>>();

            // Act
            predator.Name = "Lion";
            predator.AnimalType = AnimalType.Hunter;
            sortedDictionary.Add(predator.Name, new List<Animal> { predator });
            string expectedMessage = $"{predator.Name.ToUpper()} ADDED\n";
            var resultLionAnimal = UserInput.HandleUserInput(grid, addPredatorKey, sortedDictionary, _animalFactory);

            // Assert
            Assert.AreEqual(expectedMessage, resultLionAnimal);
        }

        [TestMethod]
        public void TestUserInput_AddPrey()
        {
            // Arrange
            ConsoleKey addPreyKey = ConsoleKey.A;
            var prey = new Prey();
            var grid = GeneratedGrid.CreateFromConsoleWidth(_rows, _columns);
            var sortedDictionary = new SortedDictionary<string, List<Animal>>();

            // Act
            prey.Name = "Antelope";
            prey.AnimalType = AnimalType.Prey;
            sortedDictionary.Add(prey.Name, new List<Animal> { prey });
            string expectedMessage = $"{prey.Name.ToUpper()} ADDED\n";
            var resultLionAnimal = UserInput.HandleUserInput(grid, addPreyKey, sortedDictionary, _animalFactory);

            // Assert
            Assert.AreEqual(expectedMessage, resultLionAnimal);
        }

        [TestMethod]
        public void TestAnimalMovement()
        {
            // Arrange
            var game = Game.Create(_grid, _rules);

            // Act
            var initialGrid = game.Initial;
            var nextGrid = _rules.Apply(initialGrid);

            // Assert
            Assert.IsNotNull(nextGrid);
            Assert.AreNotEqual(initialGrid, nextGrid);
        }

        [TestMethod]
        public void Animal_SetRandomSpeed_WithinRange()
        {
            // Act
            Predator predator = new ();
            Prey prey = new ();

            // Assert
            Assert.IsTrue(predator.Speed >= 1 && predator.Speed <= 5);
            Assert.IsTrue(prey.Speed >= 1 && prey.Speed <= 5);
        }

        [TestMethod]
        [DataRow(0, 0, new int[] { -1, 0, 1, 0, 0, -1, 0, 1 })]
        public void Animal_GetRandomPositionIndexes_ReturnsNonNullPosition(int row, int column, 
            int[] directionsData)
        {
            // Arrange
            var directions = new List<(int, int)>();

            for (int i = 0; i < directionsData.Length; i += 2) 
            {
                int x = directionsData[i];
                int y = directionsData[i + 1];
                directions.Add((x, y));
            }

            // Act
            var animalPosition = _movingService.GetRandomPositionIndexes(directions, row, column, _grid);

            // Assert
            Assert.IsNotNull(animalPosition);
        }

        [TestMethod]
        [DataRow(5, 5, 5, 5, new int[] { -1, 0, 1, 0, 0, -1, 0, 1 })]
        public void Predator_GetAnimalPosition_ReturnsCorrectPosition(int correctRow, int correctColumn,
            int wrongRow, int wrongColumn, int[] directionsArray)
        {
            // Arrange
            Predator predator = new ();
            var correctAnimalPosition = (correctRow, correctColumn);
            var wrongAnimalPosition = (wrongRow, wrongColumn);
            var directions = new List<(int, int)>();

            for (int i = 0; i < directionsArray.Length; i += 2)
            {
                var direction = (directionsArray[i], directionsArray[i + 1]);
                directions.Add(direction);
            }

            // Act
            _animalFactory.ActivateAnimal(predator, _grid.Grid, correctAnimalPosition.correctRow, 
                correctAnimalPosition.correctColumn, _grid);
            var wrongLionPosition = _movingService.GetAnimalPosition(directions, wrongAnimalPosition.wrongRow,
                wrongAnimalPosition.wrongColumn, _grid);

            // Assert
            Assert.AreEqual(correctAnimalPosition, wrongLionPosition);
        }

        [TestMethod]
        [DataRow(5, 5, 5, 5, new int[] { -1, 0, 1, 0, 0, -1, 0, 1 })]
        public void Prey_GetAnimalPosition_ReturnsCorrectPosition(int correctRow, int correctColumn,
            int wrongRow, int wrongColumn, int[] directionsArray)
        {
            // Arrange
            Prey prey = new ();
            var correctAnimalPosition = (correctRow, correctColumn);
            var wrongAnimalPosition = (wrongRow, wrongColumn);
            var directions = new List<(int, int)>();

            for (int i = 0; i < directionsArray.Length; i += 2)
            {
                var direction = (directionsArray[i], directionsArray[i + 1]);
                directions.Add(direction);
            }

            // Act
            _animalFactory.ActivateAnimal(prey, _grid.Grid, correctAnimalPosition.correctRow, 
                correctAnimalPosition.correctColumn, _grid);
            var wrongAntelopePosition = _movingService.GetAnimalPosition(directions, wrongAnimalPosition.wrongRow,
                wrongAnimalPosition.wrongColumn, _grid);

            // Assert
            Assert.AreEqual(correctAnimalPosition, wrongAntelopePosition);
        }
    }
}