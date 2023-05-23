using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movie.Dto;
using movie.Interfaces;
using movie.Models;
using movie.Repository;

namespace movie.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private IReviewRepository _ReviewRepository;
        private IMovieRepository _MovieRepository;
        private IUserRepository _UserRepository;
        private IMapper _Mapper;
       
        public ReviewController(IMapper mapper,IReviewRepository reviewRepository,IMovieRepository movieRepository,IUserRepository userRepository)
        {
          _MovieRepository = movieRepository;
           _UserRepository = userRepository;
           _ReviewRepository = reviewRepository;
            _Mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetReview()
        {
            var reviews = _ReviewRepository.GetReviews();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMapper = _Mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewMapper);
        }

        [HttpGet("{id}")]
        public IActionResult GetReview(int id)
        {
            var review = _ReviewRepository.GetReview(id);
            var reviewMapper = _Mapper.Map<ReviewViewDto>(review);
            return Ok(reviewMapper);
        }

        [HttpGet("movie/{id}")]

        public IActionResult GetReviewByMovieId(int id) {

            var reviews = _ReviewRepository.GetReviewByMovieId(id);

       
            return Ok(reviews);
        }


        [HttpPost]
        public IActionResult CreateReview([FromBody] ReviewDto reviewDto, [FromQuery] int userId, [FromQuery] int movieId  ) {
        
            var user = _UserRepository.GetUser(userId);
            var movie = _MovieRepository.GetMovie(movieId);

            var reviewMapper = _Mapper.Map<Review>(reviewDto);

            reviewMapper.Movie = movie;
            reviewMapper.User = user;


            _ReviewRepository.CreateReview(reviewMapper);

            var reviewToReturn = _ReviewRepository.GetReview(reviewMapper.Id);
            return Ok(reviewToReturn);
            
        }


        [HttpPut]
        public IActionResult UpdateReview([FromBody] ReviewDto reviewDto)
        {
            var reviewMapper = _Mapper.Map<Review>(reviewDto);
            _ReviewRepository.UpdateReview(reviewMapper);
            return Ok(reviewMapper);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           
            _ReviewRepository.DeleteReview(id);
            return Ok(id);

        }



    }
}
