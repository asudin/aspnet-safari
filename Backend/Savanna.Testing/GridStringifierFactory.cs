using Savanna.CodeLibrary;
using Savanna.ConsoleApp;
using System.Reflection;

namespace UnitTesting
{
    public class GridStringifierFactory
    {
        public static GridStringifier CreateInstance(char animalCell, char grassCell)
        {
            var constructor = typeof(GridStringifier).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null,
                new[] { typeof(char), typeof(char) }, null);

            if (constructor != null)
            {
                return (GridStringifier)constructor.Invoke(new object[] { animalCell, grassCell });
            }

            return null!;
        }
    }
}
