using Animals.CodeLibrary.Configurations;

namespace Savanna.CodeLibrary.Abstractions
{
    public interface IGame<TGameGrid, TGameRules, TAnimal> : IEnumerable<TGameGrid>
        where TGameGrid: IGameGrid<TAnimal>
        where TGameRules : IGameRules<TGameGrid, TAnimal>
        where TAnimal : Animal
    {
        TGameGrid Initial { get; }
    }
}
