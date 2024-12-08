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
    public class FavoriteFoodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteFoodController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteFood
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteFood>>> GetFavoriteFoods(int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.FavoriteFoods.Take(5).ToListAsync();
            }
            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return new List<FavoriteFood> { food };
        }

        // POST: api/FavoriteFood
        [HttpPost]
        public async Task<ActionResult<FavoriteFood>> PostFavoriteFood(FavoriteFood favoriteFood)
        {
            _context.FavoriteFoods.Add(favoriteFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavoriteFoods), new { id = favoriteFood.Id }, favoriteFood);
        }

        // PUT: api/FavoriteFood/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteFood(int id, FavoriteFood favoriteFood)
        {
            if (id != favoriteFood.Id)
            {
                return BadRequest("FavoriteFood ID mismatch.");
            }

            _context.Entry(favoriteFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteFoodExists(id))
                {
                    return NotFound($"FavoriteFood with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/FavoriteFood/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteFood(int id)
        {
            var favoriteFood = await _context.FavoriteFoods.FindAsync(id);
            if (favoriteFood == null)
            {
                return NotFound($"FavoriteFood with ID {id} not found.");
            }

            _context.FavoriteFoods.Remove(favoriteFood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a favorite food exists
        private bool FavoriteFoodExists(int id)
        {
            return _context.FavoriteFoods.Any(e => e.Id == id);
        }
    }
}