using Animals.CodeLibrary.Configurations;

namespace Savanna.CodeLibrary.Abstractions
{
    public interface IGameRules<TGameGrid, TAnimal>
        where TGameGrid : IGameGrid<TAnimal>
        where TAnimal: Animal
    {
        TGameGrid Apply(TGameGrid oldGrid);
    }
}
