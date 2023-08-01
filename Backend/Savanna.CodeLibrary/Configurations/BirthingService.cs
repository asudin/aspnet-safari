using Animals.CodeLibrary.Configurations;
using Animals.CodeLibrary.Interfaces;
using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.CodeLibrary.Configurations
{
    public class BirthingService
    {
        private readonly Random _random;
        private readonly AnimalFactory _animalFactory = new AnimalFactory();

        public BirthingService()
        {
            _random = new Random();
        }

        private void Birth(IBirthable birthable, GeneratedGrid grid, Animal[,] newGrid, (int Row, int Column) positionIndexes)
        {
            Animal? animal = birthable as Animal;
            double birthChance = 0.4;

            if (_random.NextDouble() < birthChance)
            {
                _animalFactory.ActivateAnimal(animal, newGrid, positionIndexes.Row, positionIndexes.Column, grid);
            }
        }

        public void Mate(IBirthable birthable, GeneratedGrid grid, Animal[,] newGrid, (int Row, int Column) positionIndexes)
        {
            if (birthable.MatingTimer > 0)
            {
                birthable.MatingTimer--;
            }
            else
            {
                birthable.IsMating = false;
                birthable.IsRecentlyMated = true;
                Birth(birthable, grid, newGrid, positionIndexes);
            }
        }

        public static void IsAnimalRecentlyMated(Animal animal)
        {
            if (animal.IsRecentlyMated)
            {
                animal.MatingCooldown--;

                if (animal.MatingCooldown <= 0)
                {
                    animal.IsRecentlyMated = false;
                }
            }
        }
    }
}
