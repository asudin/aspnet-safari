using Animals.CodeLibrary.Configurations;
using Savanna.CodeLibrary;
using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.ConsoleApp
{
    public class UserInput
    {
        public static string HandleUserInput(GeneratedGrid grid, ref bool keyPressed,
            SortedDictionary<string, List<Animal>> animals, AnimalFactory factory)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = true;

                return HandleUserInput(grid, keyInfo.Key, animals, factory);
            }

            return " ";
        }

        public static string HandleUserInput(GeneratedGrid grid, ConsoleKey key, SortedDictionary<string, List<Animal>> animals,
            AnimalFactory factory)
        {
            char pressedKeyChar = (char)key;

            foreach (var animalGroup in animals)
            {
                if (pressedKeyChar == animalGroup.Value[0].AnimalSymbol)
                {
                    Animal animal = animalGroup.Value[0];
                    factory.ActivateAnimal(animal, grid.Grid, grid);
                    
                    return $"{animalGroup.Value[0].Name.ToUpper()} ADDED\n";
                }
            }

            return " ";
        }
    }
}
