using AutoMapper;
using RankingApp.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RankingApp.MovieItemRepository;
using RankingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankingApp.UnitOfWork;

namespace RankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieItemController : ControllerBase
    {
        private readonly ItemContext _context;
        private readonly IMapper _mapper;
        private RankingApp.UnitOfWork.UnitOfWork _unitOfWork;

        public MovieItemController(ItemContext context, IMapper mapper, RankingApp.UnitOfWork.UnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/MovieItem
        [HttpGet]
        public ActionResult<IEnumerable<MovieItemModelDTO>> GetMovieItems()
        {
            var movieItems = _unitOfWork.MovieItemRepository.Get();
            var movieItemDTOs = _mapper.Map<List<MovieItemModelDTO>>(movieItems);
            return movieItemDTOs;
        }

        [HttpGet("{id}")]
        public ActionResult<MovieItemModelDTO> GetItemModel(int id)
        {
            var itemModel = _unitOfWork.MovieItemRepository.GetByID(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            MovieItemModelDTO movieItemModelDTO = _mapper.Map<MovieItemModelDTO>(itemModel);

            return movieItemModelDTO;
        }

        // PUT: api/AlbumItem/5
        [HttpPut("{id}")]
        public IActionResult PutMovieItemModel(int id, MovieItemModel MovieItemModel)
        {
            if (id != MovieItemModel.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.MovieItemRepository.Update(MovieItemModel);
                _unitOfWork.Save();
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
        public ActionResult<MovieItemModel> PostItemModel(MovieItemModel itemModel)
        {
            _unitOfWork.MovieItemRepository.Insert(itemModel);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.Id }, itemModel);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public IActionResult DeleteItemModel(int id)
        {
            var itemModel = _unitOfWork.MovieItemRepository.GetByID(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _unitOfWork.MovieItemRepository.Delete(id);
            _unitOfWork.Save();

            return NoContent();
        }

        // Additional methods for POST, DELETE, etc. go here...

        private bool MovieItemModelExists(int id)
        {
            return _unitOfWork.MovieItemRepository.Get(e => e.Id == id).Any();
        }

        //

    }
}