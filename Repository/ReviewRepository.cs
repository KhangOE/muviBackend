using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using movie.Data;
using movie.Dto;
using movie.Interfaces;
using movie.Models;

namespace movie.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private DataContext _context;
        public ReviewRepository(DataContext context) {  _context = context; }
       

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ReviewViewDto GetReview(int id)
        {
            return _context.Reviews.Include(r => r.Movie).Select(r => new ReviewViewDto()
            {
                Id = r.Id,
                UserName = r.User.Name,
                Text = r.Text,
                Rating = r.rating
            }).SingleOrDefault(r => r.Id == id);
        }

        public bool CreateReview(Review review)
        {   
            
            _context.Reviews.Add(review);
            return Save();
        }

        public ICollection<ReviewViewDto> GetReviewByMovieId(int id)
        {
            return _context.Reviews
                .Where(r => r.Movie.Id == id).Select(r => new ReviewViewDto() {
                Id = r.Id,
                UserName = r.User.Name,
                Text = r.Text,
                Rating = r.rating
                }).ToList();
        }
        public bool UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            return Save();
        }


        public bool DeleteReview(int id)
        {
            var review = _context.Reviews.SingleOrDefault(r => r.Id == id);
            _context.Reviews.Remove(review);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
