using AutoMapper;
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
        private readonly IMapper _mapper;

        public AlbumItemController(ItemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/AlbumItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumItemModelDTO>>> GetAlbumItems()
        {
            var albumItems = await _context.AlbumItems.ToListAsync();
            var albumItemDTOs = _mapper.Map<List<AlbumItemModelDTO>>(albumItems);
            return albumItemDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumItemModelDTO>> GetItemModel(int id)
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

            AlbumItemModelDTO albumItemModelDTO = _mapper.Map<AlbumItemModelDTO>(itemModel);
            return albumItemModelDTO;
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