namespace MovieHub.Services.Interfaces
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}
