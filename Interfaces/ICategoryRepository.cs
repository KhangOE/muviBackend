using movie.Dto;
using movie.Models;

namespace movie.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);

        ICollection<Category> GetCategoriesByMovieId(int id);
        bool CreateCategory(Category category);

        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
