using Microsoft.EntityFrameworkCore.Diagnostics;
using movie.Data;
using movie.Interfaces;
using movie.Models;

namespace movie.Repository
{
    public class CategoyRepository : ICategoryRepository
    {   

        private DataContext _context;
        public CategoyRepository(DataContext context) { 
            _context = context;
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }
        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
        public ICollection<Category> GetCategoriesByMovieId(int id)
        {
            return   _context.MoviesCategories.Where(mc => mc.MovieId == id).Select(mc => mc.Category).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
