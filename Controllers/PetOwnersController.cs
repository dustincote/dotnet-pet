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
        public PetOwnersController(ApplicationContext context)
        {
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
        public IActionResult getPetOwnerById(int id)
        {
            PetOwner petOwner = _context.petOwners.Find(id);
            if (petOwner == null){//if a pet is checked in, it is not able to be deleted
                ModelState.AddModelError("checkedIn", "Pet Checked in cannot delete");
                return ValidationProblem(ModelState);
            };
            return Ok(petOwner);
        }
        // End GET on owner by id

        //Post new owner
        [HttpPost]
        public IActionResult createOwner([FromBody] PetOwner newOwner)
        {
            Transaction note = new Transaction();
            note.transaction = $"Posting new pet owner {newOwner.name}";
            note.transactionTime = DateTime.UtcNow;//this updates the time
            _context.Add(note);
            _context.Add(newOwner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getPetOwnerById), new { id = newOwner.id }, newOwner);
        }
        //End Post new owner

        // PUT /api/petOwners/1 with a body of a owner object to update
        [HttpPut("{id}")]
        public IActionResult updateOwner([FromBody] PetOwner updateOwner)
        {

            // Make sure there *is* an owner with an id of updateOwner.id
            // If not, 404 not found

            bool exists = _context.petOwners.Any(owner => owner.id == updateOwner.id);
            if (!exists) return NotFound();

            Transaction note = new Transaction();
            note.transaction = $"Update Pet Owner {updateOwner.name}";
            note.transactionTime = DateTime.UtcNow;
            _context.Add(note);

            // _context.Entry(updateOwner).State = EntityState.Modified;
            _context.petOwners.Update(updateOwner);

            // Save it
            _context.SaveChanges();

            // Return the new owner withOk()
            return Ok(updateOwner);
        }

        [HttpDelete("{id}")]
        public IActionResult deletePetOwner(int id)
        {
            PetOwner petOwner = _context.petOwners.Find(id);
            //^--is the class we are looking ^--is the table we are deleting from
            //remove from context
            //checking to see if pet is checked in and not allowing the pet to be deleted
            bool exists = _context.pets.Any(pet => pet.petOwnerid == id && pet.checkedInAt != null);
            if (exists) return BadRequest();
            Transaction note = new Transaction();
            note.transaction = $"Delete Pet Owner {petOwner.name} ";
            note.transactionTime = DateTime.UtcNow;
            _context.Add(note);
            _context.petOwners.Remove(petOwner);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
