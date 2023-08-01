using Animals.CodeLibrary.Configurations;

namespace Savanna.CodeLibrary.Abstractions
{
    public interface IGridStringifier<TGameGrid, TAnimal>
        where TGameGrid : IGameGrid<TAnimal>
        where TAnimal : Animal
    {
        string Stringify(TGameGrid gameGrid);
    }
}
