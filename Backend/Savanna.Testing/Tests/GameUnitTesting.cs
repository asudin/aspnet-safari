using Savanna.CodeLibrary.Configurations.Savanna;

namespace GameTesting
{
    [TestClass]
    public class GameUnitTesting
    {
        private const int _rows = 20;
        private const int _columns = 40;
        private readonly GeneratedGrid _grid = GeneratedGrid.CreateFromConsoleWidth(_rows, _columns);
        private readonly GameRules _rules = GameRules.Instance;

        [TestMethod]
        public void TestGameInitialization()
        {
            // Act
            var game = Game.Create(_grid, _rules);

            // Assert
            Assert.IsNotNull(game);
            Assert.AreEqual(_grid, game.Initial);
            Assert.AreEqual(_rules, game.Rules);
        }
    }
}
