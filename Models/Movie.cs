namespace movie.Models
{
    public class Movie
    {
       public int Id {  get; set; }    
       public string Url { get; set; } 
       public string Description { get; set; }
       public string Title { get; set; }
       public int score { get; set; }
        public string Poster { get; set; }
        public string Background { get; set; }
        
        public ICollection<MovieCategory> MovieCategories { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }

        public  ICollection<MovieDirector> MovieDirectors { get; set;}

        public ICollection<Review> Reviews { get; set; }
         

      


    }
}
