﻿// <auto-generated />
using System;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    [DbContext(typeof(SoccerDbContext))]
    partial class SoccerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.GoalDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssistId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHomeTeam")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOwnGoal")
                        .HasColumnType("bit");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("ScorerId")
                        .HasColumnType("int");

                    b.Property<int?>("Time")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssistId");

                    b.HasIndex("MatchId");

                    b.HasIndex("ScorerId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.MatchDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayGoals")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HalfAwayGoals")
                        .HasColumnType("int");

                    b.Property<int>("HalfHomeGoals")
                        .HasColumnType("int");

                    b.Property<int>("HomeGoals")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("StateId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.MatchStateDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MatchStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Creating"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ongoing"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Finished"
                        });
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.PlayerDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");

                    b.HasCheckConstraint("CK_AnyName", "ISNULL([NickName], '') != '' OR ISNULL([FirstName], '') != '' OR ISNULL([LastName], '') != ''");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.RoleDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.TeamDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.TournamentDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DefaultDayOfTheWeek")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("DefaultHour")
                        .HasColumnType("time");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tournaments");

                    b.HasCheckConstraint("CK_DefaultDayOfTheWeek", "[DefaultDayOfTheWeek] >= 1 AND [DefaultDayOfTheWeek] <= 7");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.UserDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.UserRoleDbModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("PlayerDbModelTeamDbModel", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("int")
                        .HasColumnName("PlayerId");

                    b.Property<int>("TeamsId")
                        .HasColumnType("int")
                        .HasColumnName("TeamId");

                    b.HasKey("PlayersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("TeamPlayers");
                });

            modelBuilder.Entity("TournamentDbModelUserDbModel", b =>
                {
                    b.Property<int>("AdminsId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<int>("TournamentAdminsId")
                        .HasColumnType("int")
                        .HasColumnName("TournamentId");

                    b.HasKey("AdminsId", "TournamentAdminsId");

                    b.HasIndex("TournamentAdminsId");

                    b.ToTable("TournamentAdmins");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.GoalDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.PlayerDbModel", "Assist")
                        .WithMany()
                        .HasForeignKey("AssistId");

                    b.HasOne("Brilliancy.Soccer.DbModels.MatchDbModel", "Match")
                        .WithMany("Goals")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.PlayerDbModel", "Scorer")
                        .WithMany()
                        .HasForeignKey("ScorerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assist");

                    b.Navigation("Match");

                    b.Navigation("Scorer");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.MatchDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.TeamDbModel", "AwayTeam")
                        .WithMany()
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.TeamDbModel", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.MatchStateDbModel", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.TournamentDbModel", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("State");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.PlayerDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.TournamentDbModel", "Tournament")
                        .WithMany("Players")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.UserDbModel", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserId");

                    b.Navigation("Tournament");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.TeamDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.TournamentDbModel", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.TournamentDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.UserDbModel", "Owner")
                        .WithMany("OwnedTournaments")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.UserRoleDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.RoleDbModel", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.UserDbModel", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlayerDbModelTeamDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.PlayerDbModel", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.TeamDbModel", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TournamentDbModelUserDbModel", b =>
                {
                    b.HasOne("Brilliancy.Soccer.DbModels.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("AdminsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brilliancy.Soccer.DbModels.TournamentDbModel", null)
                        .WithMany()
                        .HasForeignKey("TournamentAdminsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.MatchDbModel", b =>
                {
                    b.Navigation("Goals");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.RoleDbModel", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.TournamentDbModel", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Players");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Brilliancy.Soccer.DbModels.UserDbModel", b =>
                {
                    b.Navigation("OwnedTournaments");

                    b.Navigation("Players");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
