using Animals.CodeLibrary.Configurations;
using System.ComponentModel.DataAnnotations;

namespace Animals.CodeLibrary.Abstractions
{
    public interface IAnimalPlugin
    {
        public string Name { get; }
        [Range(1, 6)]
        public int Speed { get; }
        public int MovementRange { get; }
        public double Health { get; }
        public AnimalType AnimalType { get; }
    }
}
