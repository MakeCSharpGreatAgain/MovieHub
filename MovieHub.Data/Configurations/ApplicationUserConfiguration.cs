namespace MovieHub.Data.Configurations
{
    using Models;
    using System;
    using System.Data.Entity.ModelConfiguration;

    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            //One-to-Many (User-Reviews)
            this.HasMany(u => u.Reviews)
                .WithRequired(r => r.Author)
                .HasForeignKey(r => r.AuthorId);
        }
    }
}
