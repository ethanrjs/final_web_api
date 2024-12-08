using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project_Web_API.Data;
using Final_Project_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HobbyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Hobby
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hobby>>> GetHobbies(int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.Hobbies.Take(5).ToListAsync();
            }
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }
            return new List<Hobby> { hobby };
        }

        // POST: api/Hobby
        [HttpPost]
        public async Task<ActionResult<Hobby>> PostHobby(Hobby hobby)
        {
            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHobbies), new { id = hobby.Id }, hobby);
        }

        // PUT: api/Hobby/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobby(int id, Hobby hobby)
        {
            if (id != hobby.Id)
            {
                return BadRequest("Hobby ID mismatch.");
            }

            _context.Entry(hobby).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyExists(id))
                {
                    return NotFound($"Hobby with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Hobby/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound($"Hobby with ID {id} not found.");
            }

            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a hobby exists
        private bool HobbyExists(int id)
        {
            return _context.Hobbies.Any(e => e.Id == id);
        }
    }
}