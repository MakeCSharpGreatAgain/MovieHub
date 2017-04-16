namespace MovieHub.Services.Services
{
    using Data;
    using Interfaces;
    using System;
    using Models;

    public class ApplicationUserService : IApplicationUserService
    {
        public void AddUserProfilePicture(string userId, byte[] imageData)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                var user = context.Users.Find(userId);

                if (user != null)
                {
                    user.ProfilePicture = imageData;
                    context.SaveChanges();
                }
            }
        }

        public ApplicationUser GetUserById(string id)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Users.Find(id);
            }
        }
    }
}
