namespace MovieHub.Services.Interfaces
{
    public interface IApplicationUserService
    {
        void AddUserProfilePicture(string userId, byte[] imageData);
    }
}
