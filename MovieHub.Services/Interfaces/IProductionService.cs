using MovieHub.Models;

namespace MovieHub.Services.Interfaces
{
    public interface IProductionService
    {
        Production GetProductionByName(string name);

        Production InsertProduction(string name);
    }
}
