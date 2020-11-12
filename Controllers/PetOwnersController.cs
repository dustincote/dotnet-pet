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



        
    }
}
