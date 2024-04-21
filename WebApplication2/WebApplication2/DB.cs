using WebApplication2.Animals;

namespace WebApplication2;

public class DB
{
    public static List<Animal> Animals = new()
    {
        new Animal { IdAnimal = 1, Name = "Reksio", Description = "A friendly dog", Category = "Dog", Area = "Urban" },
        new Animal { IdAnimal = 2, Name = "Dasti", Description = "A shy cat", Category = "Cat", Area = "House" },
        new Animal { IdAnimal = 3, Name = "Flapi", Description = "A loyal dog", Category = "Dog", Area = "Rural" },
    };
    
}