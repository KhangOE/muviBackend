using movie.Dto;
using movie.Models;

namespace movie.Interfaces
{
    public interface IReviewRepository
    {
       ICollection<Review> GetReviews();

        ReviewViewDto GetReview(int id);

        ICollection<ReviewViewDto> GetReviewByMovieId(int id);
        bool DeleteReview(int id);

        bool CreateReview(Review review);
        bool UpdateReview(Review review);

        bool Save();

      


    }
}
