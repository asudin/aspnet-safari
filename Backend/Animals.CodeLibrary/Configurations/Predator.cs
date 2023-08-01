namespace Animals.CodeLibrary.Configurations
{
    public class Predator : Animal
    {
        private AnimalType _predator = AnimalType.Hunter;

        public Predator()
        {
            AnimalType = _predator;
        }
    }
}
