using MovieHub.Models;

namespace MovieHub.Services.Interfaces
{
    public interface IActorService
    {
        Actor GetActorByName(string name);

        Actor InsertActor(string name);
    }
}
