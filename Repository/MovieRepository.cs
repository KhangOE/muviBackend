using Microsoft.EntityFrameworkCore;
using movie.Data;
using movie.Interfaces;
using movie.Models;

namespace movie.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Movie> GetMovies(int page, int sort, string? query, int[]? category)
        {
            //var movieFilter = _context.Movies.
            var categoryfilter = _context.MoviesCategories.
                Where(mc => category.Length == 0 ? true : category.Contains(mc.CategoryId)).
               Select(mc => mc.Movie).
                //   Where(m => m.Include(m => m.Category)).
                Where(m => query == null ? true : m.Title.Contains(query)).
              //  OrderBy(m => sort == 1 ? m.score : 0).

          
            GroupBy(m => m.Id).Select(grp => grp.First()).
             
            ToList();

            var MoviesSorted = categoryfilter.OrderBy(m => sort == 1 ? m.score : 0);
                //Include(m => m.MovieCategories.Where(mc => category.Contains(mc.CategoryId) )).
                //  Include(m => m.MovieActors).ThenInclude(m => m.Actor).
                //  Include(m => m.Reviews).

            return MoviesSorted.ToList();

           
          //  var paginate =  new ArraySegment<Movie>(result, (page - 1) * 15, (page - 1) * 15 + 15);
        }

        public ICollection<Movie> GetMovieByCategoryId(int id) {
            return _context.MoviesCategories.Where(mc => mc.CategoryId == id).Select(mc => mc.Movie).ToList();
        }

        public Movie GetMovie(int id)
        {
        
            return _context.Movies.Include(m  => m.MovieCategories).
                //ThenInclude(m => m.Category).
             //   Include(m => m.MovieActors).ThenInclude(m => m.Actor).
               // Include(m => m.Reviews).
                FirstOrDefault(m => m.Id == id);
           //return _context.Movies.Include(m => m.).Where(m => m.Id == id).FirstOrDefault();
        }

        public bool CreateMovie(Movie movie,int[] categoryId,int[] actorId) {

            foreach(int id in categoryId)
            {
                
                MovieCategory movieCategory = new MovieCategory()
                {
                    CategoryId = id,
                    Movie = movie,
                };

                _context.Add(movieCategory);

            }

            foreach (int id in actorId)
            {
                
                MovieActor movieCategory = new MovieActor()
                {
                    ActorId = id,
                    Movie = movie,
                };

                _context.Add(movieCategory);

            }



            _context.Add(movie);

            return Save();


        }


        public bool DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            
            return Save();
        }

        public bool UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
