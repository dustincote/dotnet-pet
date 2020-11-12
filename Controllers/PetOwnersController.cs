using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context) {
            _context = context;
        }

        // Begin GET all owners
        [HttpGet]
        public IEnumerable<PetOwner> getAllOwners()
        {
            Console.WriteLine("Getting owner list");
            return _context
                .petOwners
                .Include(owner => owner.pets)
                .ToList();
        }
        // End GET all owners
        
        //GET on owner by id
        [HttpGet("{id}")]
        public IActionResult getPetOwnerById(int id) {
            PetOwner petOwner = _context.petOwners.Find(id);
            if (petOwner == null) return NotFound();
            return Ok(petOwner);
        }
        // End GET on owner by id

        //Post new owner
        [HttpPost]
        public IActionResult createOwner([FromBody] PetOwner newOwner) {
            _context.Add(newOwner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getPetOwnerById), new { id = newOwner.id}, newOwner);
        }
        //End Post new owner
        
 // PUT /api/petOwners/1 with a body of a owner object to update
      [HttpPut("{id}")]
      public IActionResult updateOwner ([FromBody] PetOwner updateOwner) {

         // Make sure there *is* an owner with an id of updateOwner.id
         // If not, 404 not found
      
         bool exists = _context.petOwners.Any(owner => owner.id == updateOwner.id);
         if (!exists) return NotFound();

         // _context.Entry(updateOwner).State = EntityState.Modified;
         _context.petOwners.Update(updateOwner);
         
         // Save it
         _context.SaveChanges();

         // Return the new owner withOk()
         return Ok(updateOwner);
    }
    
    [HttpDelete("{id}")]
    public IActionResult deletePetOwner(int id) {
        PetOwner petOwner =_context.petOwners.Find(id);
        //^--is the class we are looking ^--is the table we are deleting from
        //remove from context
        _context.petOwners.Remove(petOwner);

        _context.SaveChanges();

        return NoContent();
    }
    }
}
