namespace movie.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Role { get; set; }
        public string Password { get; set; }

        public string UserName { get; set; }

        public ICollection<Review> Reviews { get; set; }


    }
}
