using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;
using RankingApp.UnitOfWork;
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
        private RankingApp.UnitOfWork.UnitOfWork _unitOfWork;

        public AlbumItemController(ItemContext context, IMapper mapper, RankingApp.UnitOfWork.UnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/AlbumItem
        [HttpGet]
        public Task<ActionResult<IEnumerable<AlbumItemModelDTO>>> GetAlbumItems()
        {
            var albumItems = _unitOfWork.AlbumItemRepository.Get();
            var albumItemDTOs = _mapper.Map<List<AlbumItemModelDTO>>(albumItems);
            return Task.FromResult<ActionResult<IEnumerable<AlbumItemModelDTO>>>(Ok(albumItemDTOs));
        }

        [HttpGet("{id}")]
        public ActionResult<AlbumItemModelDTO> GetItemModel(int id)
        {
            var itemModel = _unitOfWork.AlbumItemRepository.GetByID(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            AlbumItemModelDTO albumItemModelDTO = _mapper.Map<AlbumItemModelDTO>(itemModel);
            return albumItemModelDTO;
        }

        // PUT: api/AlbumItem/5
        [HttpPut("{id}")]
        public IActionResult PutAlbumItemModel(int id, AlbumItemModel albumItemModel)
        {
            if (id != albumItemModel.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.AlbumItemRepository.Update(albumItemModel);
                _unitOfWork.Save();
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
        public ActionResult<AlbumItemModel> PostItemModel(AlbumItemModel itemModel)
        {
            _unitOfWork.AlbumItemRepository.Insert(itemModel);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.Id }, itemModel);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public IActionResult DeleteItemModel(int id)
        {
            var itemModel = _unitOfWork.AlbumItemRepository.GetByID(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            _unitOfWork.AlbumItemRepository.Delete(itemModel);
            _unitOfWork.Save();

            return NoContent();
        }

        // Additional methods for POST, DELETE, etc. go here...

        private bool AlbumItemModelExists(int id)
        {
            return _unitOfWork.AlbumItemRepository.Get(e => e.Id == id).Any();
        }
    }
}