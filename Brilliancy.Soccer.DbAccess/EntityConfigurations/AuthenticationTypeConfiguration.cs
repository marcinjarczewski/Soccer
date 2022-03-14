using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class AuthenticationTypeConfiguration : IEntityTypeConfiguration<AuthenticationTypeDbModel>
    {
        public void Configure(EntityTypeBuilder<AuthenticationTypeDbModel> modelBuilder)
        {
            modelBuilder.HasData(
                new AuthenticationTypeDbModel
                {
                    Id = (int)AuthenticationTypeEnum.TournamentPlayerInvite,
                    Name = AuthenticationTypeEnum.TournamentPlayerInvite.ToString()
                },
                 new AuthenticationTypeDbModel
                 {
                     Id = (int)AuthenticationTypeEnum.TournamentAdminInvite,
                     Name = AuthenticationTypeEnum.TournamentAdminInvite.ToString()
                 });
        }
    }
}
