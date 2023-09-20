using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieItemController : ControllerBase
    {
        private readonly ItemContext _context;

        public MovieItemController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/AlbumItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieItemModel>>> GetMovieItems()
        {
            return await _context.MovieItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieItemModel>> GetItemModel(int id)
        {
          if (_context.MovieItems == null)
          {
              return NotFound();
          }
            var itemModel = await _context.MovieItems.FindAsync(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            return itemModel;
        }

        // PUT: api/AlbumItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieItemModel(int id, MovieItemModel MovieItemModel)
        {
            if (id != MovieItemModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(MovieItemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieItemModelExists(id))
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

         // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieItemModel>> PostItemModel(MovieItemModel itemModel)
        {
          if (_context.MovieItems == null)
          {
              return Problem("Entity set 'ItemContext.RankingItems'  is null.");
          }
            _context.MovieItems.Add(itemModel);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetItemModel", new { id = itemModel.Id }, itemModel);
            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.Id }, itemModel);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemModel(int id)
        {
            if (_context.MovieItems == null)
            {
                return NotFound();
            }
            var itemModel = await _context.MovieItems.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _context.MovieItems.Remove(itemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Additional methods for POST, DELETE, etc. go here...

        private bool MovieItemModelExists(int id)
        {
            return _context.MovieItems.Any(e => e.Id == id);
        }
    }
}