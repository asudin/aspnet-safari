using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Abstractions;
using Savanna.CodeLibrary.PluginHub;
using System.Collections;

namespace Savanna.CodeLibrary.Configurations.Savanna
{
    public sealed class GeneratedGrid : IGameGrid<Animal>
    {
        private static Animal[] _animals = Array.Empty<Animal>();
        private static AnimalFactory _animalFactory = new AnimalFactory();

        public static Animal[] Animals => _animals;
        public Animal[,] Grid { get; }
        public int Rows => Grid.GetLength(0);
        public int Columns => Grid.GetLength(1);

        private GeneratedGrid(Animal[,] grid)
        {
            Grid = grid ?? throw new ArgumentNullException(nameof(grid));
        }

        public bool IsCellEmpty(int yAxis, int xAxis) => Grid[yAxis, xAxis] == null;

        public static GeneratedGrid CreateFromConsoleWidth(int rows, int columns)
        {
            Animal[,] grid = new Animal[rows, columns];

            _animals = _animalFactory.CreateAnimalPool(PluginLoader.LoadAnimals());

            if (_animals != null)
            {
                _animalFactory.RandomlyPlaceAnimals(rows, columns, grid, _animals);
            }

            return new GeneratedGrid(grid);
        }

        public IEnumerator<Animal> GetEnumerator()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    yield return Grid[row, column];
                }
            }
        }

        public static GeneratedGrid Create(Animal[,] grid)
            => new(grid);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
