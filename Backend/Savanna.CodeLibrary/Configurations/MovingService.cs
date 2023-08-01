using System.Runtime.CompilerServices;
using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Configurations.Savanna;
using Animals.CodeLibrary.Interfaces;

[assembly: InternalsVisibleTo("Savanna.Testing")]
namespace Savanna.CodeLibrary.Configurations
{
    public class MovingService
    {
        private readonly Random _random;

        public MovingService()
        {
            _random = new Random();
        }

        private int Normalize(int current, int total)
        {
            return (current + total) % total;
        }

        private (int Row, int Column) GetMatingPosition(IBirthable birthable, List<(int Row, int Column)> directions, int row, int column, GeneratedGrid grid)
        {
            int matingTimerSeconds = 3;
            int matingCooldown = 10;
            var matingAnimalIndexes = GetAnimalPosition(directions, row, column, grid);
            var randomMatingPosition = GetRandomMatePosition(matingAnimalIndexes, grid);
            IBirthable matingAnimal = grid.Grid[matingAnimalIndexes.Row, matingAnimalIndexes.Column];

            matingAnimal.SetMatingProperties(true, matingTimerSeconds, matingCooldown);
            birthable.SetMatingProperties(true, matingTimerSeconds, matingCooldown);
            birthable.BirthPosition = ((int Row, int Column))randomMatingPosition!;

            return ((int Row, int Column))randomMatingPosition!;
        }

        private (int Row, int Column)? GetRandomMatePosition((int Row, int Column) lionPositionIndexes, GeneratedGrid grid)
        {
            List<(int RowOffset, int ColumnOffset)> matingDirections = new List<(int RowOffset, int ColumnOffset)>();
            int maxOffset = 1;

            for (int matingRow = -maxOffset; matingRow <= maxOffset; matingRow++)
            {
                for (int matingColumn = -maxOffset; matingColumn <= maxOffset; matingColumn++)
                {
                    if (matingRow == 0 && matingColumn == 0)
                    {
                        continue;
                    }

                    int newMatingRow = Normalize(lionPositionIndexes.Row + matingRow, grid.Rows);
                    int newMatingColumn = Normalize(lionPositionIndexes.Column + matingColumn, grid.Columns);

                    matingDirections.Add((newMatingRow, newMatingColumn));
                }
            }

            if (matingDirections.Count > 0)
            {
                var randomDirection = matingDirections[new Random().Next(0, matingDirections.Count)];
                return (randomDirection.RowOffset, randomDirection.ColumnOffset);
            }

            return null;
        }

        private (int Row, int Column) GetHuntingPosition(List<(int Row, int Column)> directions, int row, int column, GeneratedGrid grid)
        {
            (int Row, int Column) antelopeIndexes;

            while (true)
            {
                antelopeIndexes = GetAnimalPosition(directions, row, column, grid);
                var preyPosition = grid.Grid[antelopeIndexes.Row, antelopeIndexes.Column];

                if (preyPosition.AnimalType == AnimalType.Prey)
                {
                    preyPosition.Die();
                    grid.Grid[antelopeIndexes.Row, antelopeIndexes.Column] = null!;
                    break;
                }
            }

            return antelopeIndexes;
        }

        private void CalculatePossibleMovePositions(IMoveable moveable, List<(int RowOffset, int ColumnOffset)> directions)
        {
            for (int i = -moveable.MovementRange; i <= moveable.MovementRange; i++)
            {
                for (int j = -moveable.MovementRange; j <= moveable.MovementRange; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    directions.Add((i, j));
                }
            }
        }

        private (int Row, int Column)? GetCheckedPosition((int Row, int Column)? position, GeneratedGrid grid)
        {
            if (position.HasValue)
            {
                Animal targetAnimal = grid.Grid[position.Value.Row, position.Value.Column];

                if (targetAnimal == null)
                {
                    return position;
                }
            }

            return null;
        }

        private bool IsAnotherAnimalNearby(List<(int Row, int Column)> directions, int row, int column, GeneratedGrid grid, ref List<Animal> reachableAnimals)
        {
            reachableAnimals.Clear();

            foreach (var direction in directions)
            {
                int newRow = Normalize(row + direction.Row, grid.Rows);
                int newColumn = Normalize(column + direction.Column, grid.Columns);
                Animal? neighbor = grid.Grid[newRow, newColumn];

                if (neighbor != null)
                {
                    reachableAnimals.Add(neighbor);
                }
            }

            return reachableAnimals.Count > 0;
        }

        internal (int Row, int Column) GetAnimalPosition(List<(int Row, int Column)> directions, int row, int column, GeneratedGrid grid)
        {
            foreach (var direction in directions)
            {
                int newRow = Normalize(row + direction.Row, grid.Rows);
                int newColumn = Normalize(column + direction.Column, grid.Columns);

                if (grid.Grid[newRow, newColumn] is Animal)
                {
                    return (newRow, newColumn);
                }
            }

            return (row, column);
        }

        internal (int Row, int Column)? GetRandomPositionIndexes(List<(int RowOffset, int ColumnOffset)> directions, int row, int column, GeneratedGrid grid)
        {
            int randomIndex = _random.Next(0, directions.Count);
            var direction = directions[randomIndex];
            int newRow = Normalize(row + direction.RowOffset, grid.Rows);
            int newColumn = Normalize(column + direction.ColumnOffset, grid.Columns);

            return (newRow, newColumn);
        }

        public (int Row, int Column)? GetNewMovePosition(Animal animal, int row, int column, GeneratedGrid grid)
        {
            var directions = new List<(int RowOffset, int ColumnOffset)>();
            var reachableAnimals = new List<Animal>();

            CalculatePossibleMovePositions(animal, directions);
            BirthingService.IsAnimalRecentlyMated(animal);

            var randomPosition = GetRandomPositionIndexes(directions, row, column, grid);

            if (IsAnotherAnimalNearby(directions, row, column, grid, ref reachableAnimals))
            {
                if (reachableAnimals.Any(reachable => reachable.AnimalType == AnimalType.Prey) && 
                    animal.AnimalType == AnimalType.Hunter)
                {
                    return GetHuntingPosition(directions, row, column, grid);
                }
                else if (reachableAnimals.Any(reachable => reachable.Class == animal.Class &&
                        reachable.Name == animal.Name &&
                        !reachable.IsRecentlyMated) &&
                        !animal.IsRecentlyMated)
                {
                    return GetMatingPosition(animal, directions, row, column, grid);
                }
            }
            else
            {
                randomPosition = GetCheckedPosition(randomPosition, grid);
            }

            return randomPosition;
        }
    }
}
