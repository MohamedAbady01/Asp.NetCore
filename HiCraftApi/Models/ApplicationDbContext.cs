using System.Reflection.Emit;
using System.Xml;
using HiCraftApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HiCraftApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CraftManModel> CraftMens { get; set; }
        public DbSet<Custmer> Custmers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ImageOfPastWork> ImageOfPastWorks { get; set; }
        public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ServiceRequest>()
            .HasOne(s => s.Customer)
            .WithMany(c => c.ServiceRequests)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceRequest>()
                .HasOne(s => s.Craftman)
                .WithMany(c => c.ServiceRequests)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Review>()
           .HasOne(r => r.Craftsman)
           .WithMany(c => c.UserComment)
           .HasForeignKey(r => r.CraftsmanId)
           .OnDelete(DeleteBehavior.NoAction); // specify the ON DELETE NO ACTION option here

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
