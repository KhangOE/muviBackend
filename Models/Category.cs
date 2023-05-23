namespace movie.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<MovieCategory> MovieCategories { get; set; }
    }
}
