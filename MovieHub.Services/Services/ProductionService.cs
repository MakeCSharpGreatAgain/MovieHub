namespace MovieHub.Services.Services
{
    using Interfaces;
    using System;
    using Models;
    using Data;
    using System.Linq;

    public class ProductionService : IProductionService
    {
        public Production GetProductionByName(string name)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Productions.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            }
        }

        public Production InsertProduction(string name)
        {
            Production production = new Production()
            {
                Name = name
            };

            using (MovieDbContext context = new MovieDbContext())
            {
                context.Productions.Add(production);
                context.SaveChanges();
            }

            return production;
        }
    }
}
