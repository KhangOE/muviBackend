using movie.Models;

namespace movie.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies(int page,int sort,string? query, int[]? category);

        Movie GetMovie(int id);

        ICollection<Movie> GetMovieByCategoryId(int id);

        bool DeleteMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool CreateMovie(Movie movie, int[] actorId,  int[] categoryId);

        
        bool Save();
    }
}
