using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Abstractions;

namespace Savanna.CodeLibrary.Configurations.Savanna
{
    public sealed class GameRules : IGameRules<GeneratedGrid, Animal>
    {
        private readonly BirthingService _birthingService;
        private readonly MovingService _movingService;

        public static GameRules Instance { get; } = new();

        private GameRules()
        {
            _birthingService = new BirthingService();
            _movingService = new MovingService();
        }

        public GeneratedGrid Apply(GeneratedGrid oldGrid)
        {
            var newGrid = new Animal[oldGrid.Rows, oldGrid.Columns];

            for (int row = 0; row < oldGrid.Rows; row++)
            {
                for (int column = 0; column < oldGrid.Columns; column++)
                {
                    newGrid[row, column] = oldGrid.Grid[row, column];
                }
            }

            MoveAnimals(oldGrid, newGrid);

            return GeneratedGrid.Create(newGrid);
        }

        private void MoveAnimals(GeneratedGrid oldGrid, Animal[,] newGrid)
        {
            double moveDamage = 0.5;
            var animals = oldGrid.Grid.Cast<Animal>()
                .Where(animal => animal != null && animal.IsAlive)
                .ToList();

            animals.Sort((a1, a2) => a2.Speed.CompareTo(a1.Speed));

            foreach (var animal in animals)
            {
                var currentPosition = FindAnimalPosition(animal, oldGrid);

                if (currentPosition.HasValue && animal.IsMating == false)
                {
                    var newPosition = _movingService.GetNewMovePosition(animal, currentPosition?.Row ?? 0, currentPosition?.Column ?? 0, oldGrid);

                    if (newPosition != null && animal.Health > 0)
                    {
                        newGrid[newPosition.Value.Row, newPosition.Value.Column] = animal;
                        newGrid[currentPosition?.Row ?? 0, currentPosition?.Column ?? 0] = null!;
                        animal.TakeDamage(moveDamage);
                    }
                    else
                    {
                        animal.Die();
                        newGrid[currentPosition?.Row ?? 0, currentPosition?.Column ?? 0] = null!;
                    }
                }
                else if (animal.IsMating)
                {
                    _birthingService.Mate(animal, oldGrid, newGrid, animal.BirthPosition);
                }
            }
        }

        private static (int Row, int Column)? FindAnimalPosition(Animal animal, GeneratedGrid grid)
        {
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    if (grid.Grid[row, column] == animal)
                    {
                        return (row, column);
                    }
                }
            }

            return null;
        }
    }
}
