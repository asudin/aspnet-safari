using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Abstractions;
using Savanna.CodeLibrary.Configurations.Savanna;
using System.Text;

namespace Savanna.CodeLibrary
{
    public sealed class GridStringifier : IGridStringifier<GeneratedGrid, Animal>
    {
        private readonly SortedDictionary<string, List<Animal>> _groupedAnimals;

        public char GrassCell { get; }
        public char AnimalCell { get; }

        private GridStringifier(char animalCell, char grassCell)
        {
            _groupedAnimals = new();
            AnimalCell = animalCell;
            GrassCell = grassCell;
        }

        private char GetPrintedCharacter(Animal cell)
        {
            return cell?.AnimalSymbol ?? GrassCell;
        }

        private void FillAnimalDictionary(SortedDictionary<string, List<Animal>> groupedAnimals, Animal animal)
        {
            var animalName = (animal).Name;

            if (groupedAnimals.ContainsKey(animalName))
            {
                _groupedAnimals[animalName].Add(animal);
            }
            else
            {
                _groupedAnimals.Add(animalName, new List<Animal> { animal });
            }
        }

        public SortedDictionary<string, List<Animal>> GetCurrentAnimalList()
        {
            return _groupedAnimals;
        }

        public List<string> GetDisplayedAnimalsList()
        {
            var displayedAnimals = new List<string>();

            foreach (var animalGroup in _groupedAnimals)
            {
                displayedAnimals.Add(animalGroup.Key);
            }

            return displayedAnimals;
        }

        public string DisplayAnimalInfo()
        {
            var stringBuilder = new StringBuilder();

            foreach (var animalGroup in _groupedAnimals)
            {
                stringBuilder.Append($"{animalGroup.Key}(s) : {animalGroup.Value.Count} || ");
            }

            return stringBuilder.AppendLine().ToString();
        }

        private void StringifyCell(StringBuilder stringBuilder, char printedCharacter, Animal cell,
            SortedDictionary<string, List<Animal>> groupedAnimals)
        {
            stringBuilder.Append(printedCharacter);

            if (cell is Animal)
            {
                FillAnimalDictionary(groupedAnimals, cell);
            }
        }

        public string Stringify(GeneratedGrid grid)
        {
            var stringBuilder = new StringBuilder();

            _groupedAnimals.Clear();

            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    var cell = grid.Grid[row, column];
                    var printedCharacter = GetPrintedCharacter(cell);

                    StringifyCell(stringBuilder, printedCharacter, cell, _groupedAnimals);

                    if (column == grid.Columns - 1)
                    {
                        stringBuilder.AppendLine();
                    }
                }
            }

            return stringBuilder.ToString();
        }

        public static GridStringifier AddCellChar()
            => new(' ', '.'); // replace grassCell with . or ⛬
    }
}
