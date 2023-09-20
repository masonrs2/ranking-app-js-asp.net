using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;

namespace TutApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemContext _context;

        public ItemController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetRankingItems()
        {
          if (_context.RankingItems == null)
          {
              return NotFound();
          }
            return await _context.RankingItems.ToListAsync();
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> GetItemModel(int id)
        {
          if (_context.RankingItems == null)
          {
              return NotFound();
          }
            var itemModel = await _context.RankingItems.FindAsync(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            return itemModel;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemModel(int id, ItemModel itemModel)
        {
            if (id != itemModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemModelExists(id))
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
        public async Task<ActionResult<ItemModel>> PostItemModel(ItemModel itemModel)
        {
          if (_context.RankingItems == null)
          {
              return Problem("Entity set 'ItemContext.RankingItems'  is null.");
          }
            _context.RankingItems.Add(itemModel);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetItemModel", new { id = itemModel.Id }, itemModel);
            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.Id }, itemModel);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemModel(int id)
        {
            if (_context.RankingItems == null)
            {
                return NotFound();
            }
            var itemModel = await _context.RankingItems.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _context.RankingItems.Remove(itemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemModelExists(int id)
        {
            return (_context.RankingItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
