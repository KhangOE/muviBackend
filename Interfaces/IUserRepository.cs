using movie.Models;

namespace movie.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);

        ICollection<User> GetUsers();

        bool CreateUser(User user); 

        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Login(User user);
       

        

    }
}
