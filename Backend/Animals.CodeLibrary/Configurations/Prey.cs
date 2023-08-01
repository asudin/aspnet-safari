using Animals.CodeLibrary.Abstractions;

namespace Animals.CodeLibrary.Configurations
{
    public class Prey : Animal
    {
        private AnimalType _prey = AnimalType.Prey;

        public Prey()
        {
            AnimalType = _prey;
        }
    }
}
