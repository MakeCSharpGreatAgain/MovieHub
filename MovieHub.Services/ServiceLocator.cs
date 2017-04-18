namespace MovieHub.Services
{
    using Interfaces;
    using Services;
    using System;
    using System.Collections.Generic;

    public class ServiceLocator : IServiceLocator
    {
        private static ServiceLocator instance = new ServiceLocator();
        private IDictionary<object, object> services;

        static ServiceLocator()
        {
        }

        private ServiceLocator()
        {
            services = new Dictionary<object, object>();

            //Add services:
            services.Add(typeof(IApplicationUserService), new ApplicationUserService());
            services.Add(typeof(IMovieService), new MovieService());
            services.Add(typeof(IActorService), new ActorService());
            services.Add(typeof(IDirectorService), new DirectorService());
            services.Add(typeof(IGenreService), new GenreService());
            services.Add(typeof(IProductionService), new ProductionService());
            services.Add(typeof(IReviewService), new ReviewService());
        }

        public static ServiceLocator Instance
        {
            get
            {
                return instance;
            }
        }

        public T GetService<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new NotImplementedException("The requested service is not registered!");
            }
        }
    }
}
