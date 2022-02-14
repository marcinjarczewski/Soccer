using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleDbModel>
    {
        public void Configure(EntityTypeBuilder<RoleDbModel> modelBuilder)
        {
            modelBuilder.HasData(
                new RoleDbModel
                {
                    Id = (int)RoleEnum.Admin,
                    Name = RoleEnum.Admin.ToString()
                });
        }
    }
}
