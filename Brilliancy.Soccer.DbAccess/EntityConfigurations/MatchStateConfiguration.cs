using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class MatchStateConfiguration : IEntityTypeConfiguration<MatchStateDbModel>
    {
        public void Configure(EntityTypeBuilder<MatchStateDbModel> modelBuilder)
        {
            modelBuilder.HasData(
               new MatchStateDbModel
               {
                   Id = (int)MatchStateEnum.Creating,
                   Name = MatchStateEnum.Creating.ToString()
               },
               new MatchStateDbModel
               {
                   Id = (int)MatchStateEnum.Ongoing,
                   Name = MatchStateEnum.Ongoing.ToString()
               },
               new MatchStateDbModel
               {
                   Id = (int)MatchStateEnum.Finished,
                   Name = MatchStateEnum.Finished.ToString()
               },
               new MatchStateDbModel
               {
                   Id = (int)MatchStateEnum.Canceled,
                   Name = MatchStateEnum.Canceled.ToString()
               },
               new MatchStateDbModel
               {
                   Id = (int)MatchStateEnum.Pending,
                   Name = MatchStateEnum.Pending.ToString()
               });
        }
    }
}
