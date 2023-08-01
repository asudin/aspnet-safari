using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.CodeLibrary
{
    public static class UserInputService
    {
        public static void HandleUserAnimalInput(char key, GeneratedGrid grid, SortedDictionary<string, List<Animal>> animals,
            AnimalFactory factory)
        {
            foreach (var animalGroup in animals)
            {
                if (char.ToUpper(key) == animalGroup.Value[0].AnimalSymbol)
                {
                    Animal animal = animalGroup.Value[0];
                    factory.ActivateAnimal(animal, grid.Grid, grid);
                }
            }
        }
    }
}
