using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Shared.Enums;
using Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalShelterContext _context;

        public AnimalsController(AnimalShelterContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimal(string animalType, string gender, string canBeWithKids)
        {
            List<Animal> FoundAnimals = await _context.Animal.Include(x => x.Remarks).ToListAsync();
            if (animalType != null) FoundAnimals = FoundAnimals.FindAll(a => a.AnimalType.Equals(Enum.Parse(typeof(AnimalType), animalType)));
            if (gender != null) FoundAnimals = FoundAnimals.FindAll(a => a.Gender.Equals(Enum.Parse(typeof(Gender), gender)));
            if (canBeWithKids != null) FoundAnimals = FoundAnimals.FindAll(a => a.CanBeWithKids.Equals(Enum.Parse(typeof(DeepBoolean), canBeWithKids)));
            return FoundAnimals;
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animal.FindAsync(id);

            if (animal == null)
            {
                return null;
            }

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Animals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> PostAnimal(Animal animal)
        {
            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();

            ResponseModel Model = new ResponseModel();
            Model.Id = animal.Id;

            return Model;
            //return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();

            return animal;
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}
