using movie.Models;

namespace movie.Interfaces
{
    public interface IActorRepository
    {
        ICollection<Actor> GetActors();
        Actor GetActor(int id);
        bool CreateActor(Actor actor);

        bool UpdateActor(Actor actor);
        bool DeleteActor(int Id);
    }
}
