using BCrypt.Net;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using movie.Data;
using movie.Interfaces;
using movie.Models;

namespace movie.Repository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext dbContext)
        {
            _context = dbContext;
        }
        public User GetUser(int id)
        {
            return  _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers() {
            return _context.Users.ToList();
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();  
        }

        public bool Login(User model)
        {
            
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName);
            if (user == null ||  !BCrypt.Net.BCrypt.Verify(model.Password, user.Password)) return false;
            
            return true;
        }

        bool Save()
        {
            return _context.SaveChanges() > 1 ? true :false; 
        }
    }
}
