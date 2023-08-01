using Animals.CodeLibrary.Configurations;

namespace Animals.CodeLibrary.Interfaces
{
    public interface IMoveable
    {
        public int MovementRange { get; }
        public int Speed { get; }
    }
}
