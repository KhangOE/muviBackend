using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie.Dto;
using movie.Interfaces;
using movie.Models;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieRepository _movieRepository;
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public MovieController(IMovieRepository movieRepository,IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _categoryRepository = categoryRepository;
        
        }

        [HttpGet]
        public IActionResult GetMovies([FromQuery] int page, [FromQuery] int[]? category, [FromQuery] string? query,[FromQuery] int sort)
        {
            var movies = _movieRepository.GetMovies(page, sort ,query, category);
            var total = movies.Count();
            var moviePaginate = movies.Skip((page - 1) * 15).Take(15);
            //  var moviesMapper = _mapper.Map<MovieDto>(movies);
           
            return Ok(new {total, moviePaginate,query  });
        }

        [HttpGet("{Id}")]
        public IActionResult GetMovie(int Id)
        {
           var movie = _movieRepository.GetMovie(Id);

            var r = new MovieDto()
            {
                Id = movie.Id,
                Url = movie.Url,
                Poster = movie.Poster,
                Background = movie.Background,
                Description = movie.Description,
                Title = movie.Title,
                Categories =_mapper.Map<List<CategoryDto>>( _categoryRepository.GetCategoriesByMovieId(Id))
            };
           
            return Ok(r);
        }


        [HttpGet("category/{Id}")]
        public IActionResult GetMoviesByCategoryId(int Id) {
               
            var movies = _movieRepository.GetMovieByCategoryId(Id);

           // var moviesMapper = _mapper.Map<List<MovieDto>>(movies);

            return Ok(movies);
            
        }
        

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] MovieDto movie)
        {
           //var movie = _movieRepository.GetMovie(movieId);
           var movieMapper = _mapper.Map<Movie>(movie);
            _movieRepository.UpdateMovie(movieMapper);
            return Ok("update");

        }
        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieUploadDto movie, [FromQuery]int[] categoryId,[FromQuery] int[] actorId)
        {
            var movieMapper = _mapper.Map<Movie>(movie);
            _movieRepository.CreateMovie(movieMapper, categoryId, actorId);
            return Ok("success");
        }

        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovie(int movieId)
        {
            var movie = _movieRepository.GetMovie(movieId);
            _movieRepository.DeleteMovie(movie);
            return Ok("deleted");
        }
    }
}
