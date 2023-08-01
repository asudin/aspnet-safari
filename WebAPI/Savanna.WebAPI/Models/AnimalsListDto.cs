namespace Savanna.WebAPI.Models
{
    public class AnimalsListDto
    {
        public List<string> AnimalsList { get; set; }

        public AnimalsListDto(List<string> animalsList)
        {
            AnimalsList = animalsList;
        }
    }
}
