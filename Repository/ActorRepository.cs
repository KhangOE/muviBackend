using Microsoft.EntityFrameworkCore;
using movie.Data;
using movie.Interfaces;
using movie.Models;

namespace movie.Repository
{
    public class ActorRepository : IActorRepository
    {
        private DataContext _dataContext;
        public ActorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Actor> GetActors()
        {

            return _dataContext.Actors.ToList();
        }

        public Actor GetActor(int id)
        {
            return _dataContext.Actors.SingleOrDefault(a => a.Id == id);
        }

        public bool DeleteActor(int id)
        {
            var actor = _dataContext.Actors.SingleOrDefault(a =>a.Id == id);
            _dataContext.Actors.Remove(actor);
            return Save();
        }
        public bool CreateActor(Actor actor)
        {
            _dataContext.Add(actor);
            return Save();
        } 

        public bool UpdateActor(Actor actor)
        {
            _dataContext.Update(actor);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
