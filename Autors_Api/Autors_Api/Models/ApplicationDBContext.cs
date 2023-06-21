using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Autors_Api.Models
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet <Author> Authors { get; set; }
        public DbSet<NewsModel> News { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserLogin<String>>().ToTable("UsersLogin");
            builder.Entity<IdentityUserClaim<String>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<String>>().ToTable("UserRole");
            builder.Entity<IdentityUserToken<String>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<String>>().ToTable("RoleClaim");
        }
    }
}
