using Animals.CodeLibrary.Configurations;

namespace Savanna.CodeLibrary.Abstractions
{
    public interface IGameGrid<TAnimal> : IEnumerable<TAnimal>
        where TAnimal : Animal
    {
        public int Rows { get; }
        public int Columns { get; }
    }
}
