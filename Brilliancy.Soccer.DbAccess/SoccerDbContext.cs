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
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());
            modelBuilder.ApplyConfiguration(new MatchStateConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
        }
        public DbSet<UserDbModel> Users { get; set; }

        public DbSet<RoleDbModel> Roles { get; set; }

        public DbSet<UserRoleDbModel> UserRoles { get; set; }

        public DbSet<PlayerDbModel> Players { get; set; }

        public DbSet<TournamentDbModel> Tournaments { get; set; }

        public DbSet<MatchDbModel> Matches { get; set; }

        public DbSet<TeamDbModel> Teams { get; set; }

        public DbSet<GoalDbModel> Goals { get; set; }

        public DbSet<FileDbModel> Files { get; set; }

        public DbSet<MatchStateDbModel> MatchStates { get; set; }

        public DbSet<ConfigurationDbModel> Configurations { get; set; }

        public DbSet<TranslationDbModel> Translations { get; set; }

        public DbSet<TranslationEntryDbModel> TranslationEntries { get; set; }

        public DbSet<LanguageDbModel> Languages { get; set; }
    }
}
