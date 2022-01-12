using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
namespace Brilliancy.Soccer.DbAccess
{
    public class SoccerDbContext : DbContext
    {
        public SoccerDbContext(DbContextOptions<SoccerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleDbModel>()
                .HasKey(bc => new { bc.UserId, bc.RoleId });
            modelBuilder.Entity<UserRoleDbModel>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserRoleDbModel>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<RoleDbModel>().HasData(
                new RoleDbModel { 
                    Id = (int)RoleEnum.Admin,
                    Name = RoleEnum.Admin.ToString()
                });
        }
        public DbSet<UserDbModel> Users { get; set; }

        public DbSet<RoleDbModel> Roles { get; set; }

        public DbSet<UserRoleDbModel> UserRoles { get; set; }
    }
}
