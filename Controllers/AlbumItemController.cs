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
    public class AlbumItemController : ControllerBase
    {
        private readonly ItemContext _context;

        public AlbumItemController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/AlbumItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumItemModel>>> GetAlbumItems()
        {
            return await _context.AlbumItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumItemModel>> GetItemModel(int id)
        {
          if (_context.AlbumItems == null)
          {
              return NotFound();
          }
            var itemModel = await _context.AlbumItems.FindAsync(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            return itemModel;
        }

        // PUT: api/AlbumItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbumItemModel(int id, AlbumItemModel albumItemModel)
        {
            if (id != albumItemModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(albumItemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumItemModelExists(id))
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
        public async Task<ActionResult<AlbumItemModel>> PostItemModel(AlbumItemModel itemModel)
        {
          if (_context.AlbumItems == null)
          {
              return Problem("Entity set 'ItemContext.RankingItems'  is null.");
          }
            _context.AlbumItems.Add(itemModel);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetItemModel", new { id = itemModel.Id }, itemModel);
            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.Id }, itemModel);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemModel(int id)
        {
            if (_context.AlbumItems == null)
            {
                return NotFound();
            }
            var itemModel = await _context.AlbumItems.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _context.AlbumItems.Remove(itemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Additional methods for POST, DELETE, etc. go here...

        private bool AlbumItemModelExists(int id)
        {
            return _context.AlbumItems.Any(e => e.Id == id);
        }
    }
}