using Microsoft.AspNetCore.Mvc;
using WebApplication2.Animals;

namespace WebApplication2.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    // GET all animals
    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(DB.Animals);
    }

    // GET a single animal by IdAnimal
    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = DB.Animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animal == null)
        {
            return NotFound($"Animal with Id {id} was not found.");
        }

        return Ok(animal);
    }

    // POST a new animal
    [HttpPost]
    public IActionResult PostAnimal(Animal animal)
    {
        if (DB.Animals.Any(a => a.IdAnimal == animal.IdAnimal))
        {
            return Conflict($"Animal with id {animal.IdAnimal} is already present in the database.");
        }

        DB.Animals.Add(animal);
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.IdAnimal }, animal);
    }

    // PUT to update an existing animal
    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, [FromBody] Animal animalPatch)
    {
        var existingAnimal = DB.Animals.FirstOrDefault(a => a.IdAnimal == id);
        if (existingAnimal == null)
        {
            return NotFound($"Animal with Id {id} was not found.");
        }

        existingAnimal = ApplyPatchData(existingAnimal, animalPatch);
        return Ok("Animal updated successfully.");
    }

    private Animal ApplyPatchData(Animal existingAnimal, Animal animalPatch)
    {
        if (!string.IsNullOrWhiteSpace(animalPatch.Name))
        {
            existingAnimal.Name = animalPatch.Name;
        }

        if (!string.IsNullOrWhiteSpace(animalPatch.Description))
        {
            existingAnimal.Description = animalPatch.Description;
        }

        if (!string.IsNullOrWhiteSpace(animalPatch.Category))
        {
            existingAnimal.Category = animalPatch.Category;
        }

        if (!string.IsNullOrWhiteSpace(animalPatch.Area))
        {
            existingAnimal.Area = animalPatch.Area;
        }

        return existingAnimal;
    }

    // DELETE an animal
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = DB.Animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animal == null)
        {
            return NotFound($"Animal with Id {id} was not found.");
        }

        DB.Animals.Remove(animal);
        return Ok($"Animal with id {id} was successfully deleted from the database.");
    }
}