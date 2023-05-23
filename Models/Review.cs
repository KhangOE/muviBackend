namespace movie.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int rating { get; set; }
        public User User { get; set; }

        public Movie Movie { get; set; } 
    }
}
