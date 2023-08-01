using Animals.CodeLibrary.Abstractions;
using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.CodeLibrary
{
    public class AnimalFactory
    {
        private Random _random = new();

        public AnimalFactory() { }

        private static List<TAnimal> CreateAnimalsFromImports<TItem, TAnimal>(List<TItem> importedList, int targetCount,
            Func<TItem, TAnimal> creatredAnimalType)
        {
            return importedList
                .Take(targetCount)
                .Select(creatredAnimalType)
                .Cast<TAnimal>()
                .ToList();
        }

        private static List<T> DuplicateAnimals<T>(List<T> animalList, int targetCount)
        {
            while (animalList.Count < targetCount)
            {
                int remainingItems = targetCount - animalList.Count;
                var duplicateItems = animalList.Take(remainingItems).ToList();
                animalList.AddRange(duplicateItems);
            }

            return animalList;
        }

        private void GenerateAnimalRandomPosition(out int randomRow, out int randomColumn, GeneratedGrid grid)
        {
            randomRow = _random.Next(0, grid.Rows);
            randomColumn = _random.Next(0, grid.Columns);

            while (!grid.IsCellEmpty(randomRow, randomColumn))
            {
                randomRow = _random.Next(0, grid.Rows);
                randomColumn = _random.Next(0, grid.Columns);
            }
        }

        public void RandomlyPlaceAnimals(int rows, int columns, Animal[,] grid, Animal[] animals)
        {
            int numberOfPredators = 4;
            int numberOfPreys = 5;

            List<Animal> selectedPredators = animals
                .Where(animal => animal.AnimalType == AnimalType.Hunter)
                .OrderBy(_ => _random.Next())
                .Take(numberOfPredators)
                .ToList();

            List<Animal> selectedPreys = animals
                .Where(animal => animal.AnimalType == AnimalType.Prey)
                .OrderBy(_ => _random.Next())
                .Take(numberOfPreys)
                .ToList();

            List<Animal> randomlySelectedAnimals = selectedPredators.Concat(selectedPreys).ToList();

            foreach (var animal in randomlySelectedAnimals)
            {
                int randomRow = _random.Next(0, rows);
                int randomColumn = _random.Next(0, columns);

                while (grid[randomRow, randomColumn] != null)
                {
                    randomRow = _random.Next(0, rows);
                    randomColumn = _random.Next(0, columns);
                }

                grid[randomRow, randomColumn] = animal;
                animal.IsAlive = true;
            }
        }

        public Animal[] CreateAnimalPool(IEnumerable<IAnimalPlugin> animals)
        {
            var pooledAnimals = Array.Empty<Animal>();
            int animalsImportNumber = 50;
            int predatorsTotalNumber = 50;
            int preysTotalNumber = 50;
            List<Predator> predatorList = new();
            List<Prey> preyList = new();

            if (animals == null)
            {
                return null!;
            }

            var importedPredators = animals.Where(animal => animal.AnimalType == AnimalType.Hunter)
                                   .OrderBy(_ => Guid.NewGuid())
                                   .Take(animalsImportNumber)
                                   .ToList();
            var importedPreys = animals.Where(animal => animal.AnimalType == AnimalType.Prey)
                                       .OrderBy(_ => Guid.NewGuid())
                                       .Take(animalsImportNumber)
                                       .ToList();

            if (importedPredators.Any())
            {
                importedPredators = DuplicateAnimals(importedPredators, predatorsTotalNumber);

                predatorList = CreateAnimalsFromImports(importedPredators, importedPredators.Count, predator =>
                new Predator
                {
                    Name = predator.Name,
                    AnimalSymbol = predator.Name[0],
                    Speed = predator.Speed,
                    MovementRange = predator.MovementRange,
                    Health = predator.Health,
                    MaxHealth = predator.Health
                });
            }

            if (importedPreys.Any())
            {
                importedPreys = DuplicateAnimals(importedPreys, preysTotalNumber);

                preyList = CreateAnimalsFromImports(importedPreys, importedPreys.Count, prey =>
                    new Prey
                    {
                        Name = prey.Name,
                        AnimalSymbol = prey.Name[0],
                        Speed = prey.Speed,
                        MovementRange = prey.MovementRange,
                        Health = prey.Health,
                        MaxHealth = prey.Health
                    });
            }

            pooledAnimals = predatorList.Cast<Animal>().Concat(preyList).ToArray();

            return pooledAnimals;
        }

        public void ActivateAnimal(Animal animal, Animal[,] newGrid, GeneratedGrid grid)
        {
            ActivateAnimal(animal, newGrid, null, null, grid);
        }

        public void ActivateAnimal(Animal? animal, Animal[,] newGrid, int? row, int? column,
            GeneratedGrid grid)
        {
            Animal? nextInactiveAnimal = null;

            if (animal != null)
            {
                nextInactiveAnimal = GeneratedGrid.Animals.FirstOrDefault(nextAnimal => nextAnimal != null &&
                nextAnimal.Name == animal.Name &&
                nextAnimal.IsAlive == false
                );
            }

            if (nextInactiveAnimal != null)
            {
                int randomRow;
                int randomColumn;

                if (row.HasValue && column.HasValue)
                {
                    randomRow = row.Value;
                    randomColumn = column.Value;
                }
                else
                {
                    GenerateAnimalRandomPosition(out randomRow, out randomColumn, grid);
                }

                newGrid[randomRow, randomColumn] = nextInactiveAnimal;
                nextInactiveAnimal.IsAlive = true;
            }
        }
    }
}
