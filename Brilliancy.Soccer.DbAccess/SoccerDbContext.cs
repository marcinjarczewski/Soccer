using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbAccess.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
        }
        public DbSet<UserDbModel> Users { get; set; }

        public DbSet<RoleDbModel> Roles { get; set; }

        public DbSet<UserRoleDbModel> UserRoles { get; set; }

        public DbSet<TournamentDbModel> Tournaments { get; set; }
    }
}
