using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Abstractions;
using System.Collections;

namespace Savanna.CodeLibrary.Configurations.Savanna
{
    public class Game : IGame<GeneratedGrid, GameRules, Animal>
    {
        private readonly GameRules _gameRules;

        public GeneratedGrid Initial { get; private set; }
        public GameRules Rules => _gameRules;

        private Game(GeneratedGrid? grid, GameRules rules)
        {
            Initial = grid ?? throw new ArgumentNullException(nameof(grid));
            _gameRules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public static Game Create(GeneratedGrid? grid, GameRules rules)
            => new(grid, rules);

        public IEnumerator<GeneratedGrid> GetEnumerator()
        {
            var current = Initial;

            while (true)
            {
                yield return current;

                current = _gameRules.Apply(current);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
         => GetEnumerator();
    }
}
