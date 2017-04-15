namespace MovieHub.Services.Services
{
    using Data;
    using Interfaces;
    using Models;
    using System.Linq;

    public class ActorService : IActorService
    {
        public Actor GetActorByName(string name)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Actors.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
            }
        }

        public Actor InsertActor(string name)
        {
            Actor actor = new Actor()
            {
                Name = name
            };

            using (MovieDbContext context = new MovieDbContext())
            {
                context.Actors.Add(actor);
                context.SaveChanges();
            }

            return actor;
        }
    }
}

