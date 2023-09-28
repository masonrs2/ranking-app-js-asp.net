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

namespace RankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieItemController : ControllerBase
    {
        private readonly ItemContext _context;
        private readonly IMapper _mapper;
        private IMovieItemRepository movieItemRepository;

        // public MovieItemController()
        // {
        //     this.movieItemRepository = new RankingApp.MovieItemRepository.MovieItemRepository(_context);
        // }

        public MovieItemController(ItemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.movieItemRepository = new RankingApp.MovieItemRepository.MovieItemRepository(context);
        }

        // GET: api/MovieItem
        [HttpGet]
        public ActionResult<IEnumerable<MovieItemModelDTO>> GetMovieItems()
        {
            var movieItems = movieItemRepository.GetMovies();
            var movieItemDTOs = _mapper.Map<List<MovieItemModelDTO>>(movieItems);
            return movieItemDTOs;
        }

        [HttpGet("{id}")]
        public ActionResult<MovieItemModelDTO> GetItemModel(int id)
        {
            var itemModel = movieItemRepository.GetMovieByID(id);

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

            movieItemRepository.UpdateMovie(MovieItemModel);
            try
            {
                movieItemRepository.Save();
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
            movieItemRepository.InsertMovie(itemModel);
            movieItemRepository.Save();

            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.Id }, itemModel);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public IActionResult DeleteItemModel(int id)
        {
            var itemModel = movieItemRepository.GetMovieByID(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            movieItemRepository.DeleteMovie(id);
            movieItemRepository.Save();

            return NoContent();
        }

        // Additional methods for POST, DELETE, etc. go here...

        private bool MovieItemModelExists(int id)
        {
            return movieItemRepository.GetMovieByID(id) != null;
        }

        //

    }
}