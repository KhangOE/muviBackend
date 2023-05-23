﻿namespace movie.Models
{
    public class Actor
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
