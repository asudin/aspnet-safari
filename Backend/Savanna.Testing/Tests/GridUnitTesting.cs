using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary;
using Savanna.CodeLibrary.Configurations.Savanna;
using Savanna.ConsoleApp;
using UnitTesting;

namespace GameTesting
{
    [TestClass]
    public class GridUnitTesting
    {
        private const int _rows = 20;
        private const int _columns = 40;
        private readonly GeneratedGrid _grid = GeneratedGrid.CreateFromConsoleWidth(_rows, _columns);

        [TestMethod]
        public void TestGridStringification()
        {
            // Arrange
            var stringifier = GridStringifier.AddCellChar();

            // Act
            var gridString = stringifier.Stringify(_grid);

            // Assert
            Assert.IsNotNull(gridString);
        }

        [TestMethod]
        public void TestGridCellCharacterConfiguration()
        {
            // Arrange
            var animalCell = 'A';
            var grassCell = '.';
            var stringifier = GridStringifierFactory.CreateInstance(animalCell, grassCell);

            // Act
            var gridString = stringifier.Stringify(_grid);

            // Assert
            Assert.IsNotNull(gridString);
        }
    }
}
