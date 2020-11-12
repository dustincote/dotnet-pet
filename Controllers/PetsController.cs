using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // get all pets
        [HttpGet]//returning a list of pets from Pet class
        public IEnumerable<Pet> getPet(){
            return _context.pets.Include(p => p.petOwner).OrderBy(p => p.name).ToList();
        }

        //get pet by id
        [HttpGet("{id}")]
        public IActionResult getPetById(int id)
        {
            Pet pet = _context.pets.Find(id);
            if(pet == null)return NotFound();
            return Ok(pet);
        }

        // POST pet
        [HttpPost]
        public IActionResult createBread([FromBody] Pet newAnimal)
        {
            // TODO: Make sure the newAnimal has an owner!
            PetOwner animal = _context.petOwners.Find(newAnimal.petOwnerid);
            // if (animal == null) return BadRequest();
            // Lets add a custom error message if the new animal is invalid!
            if (animal == null) {
                ModelState.AddModelError("petOwnerid", "Invalid Owner ID");
                return ValidationProblem(ModelState);
            }
            
            _context.Add(newAnimal);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getPetById), new{ id= newAnimal.id}, newAnimal);
        }

        // DELETE Pet
        [HttpDelete("{id}")]
        public IActionResult deletePet(int id) {
            Pet animal = _context.pets.Find(id);
            if (animal == null) return NotFound();

            // Remove it from the context
            _context.pets.Remove(animal);

            // Save Context
            _context.SaveChanges();
           return NoContent();
        }
        //update pet
        [HttpPut("{id}")]
              public IActionResult updatePet ([FromBody] Pet updatePet) {

         // Make sure there *is* a pet with an id of updatePet.id
         // If not, 404 not found
      
         bool exists = _context.pets.Any(pet => pet.id == updatePet.id);
         if (!exists) return NotFound();

         // _context.Entry(updatePet).State = EntityState.Modified;
         _context.pets.Update(updatePet);
         
         // Save it
         _context.SaveChanges();

         // Return the new pet withOk()
         return Ok(updatePet);
    }
        
        //update checkedInAt to be checked in
        [HttpPut("{id}/checkin")]
        public IActionResult checkIn (int id) {
            Pet pet = _context.pets.Find(id);
            pet.checkIn();
            _context.Update(pet);
            _context.SaveChanges();
            return Ok(pet)
;        }

 // update checkedInAt to be checked out
        [HttpPut("{id}/checkout")]
        public IActionResult checkOut( int id)
        {
            Pet pet = _context.pets.Find(id);
            pet.checkOut();
            _context.Update(pet);
            _context.SaveChanges();
            return Ok(pet);
        }

    }

}


