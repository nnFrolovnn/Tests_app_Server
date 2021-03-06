﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tests_server_app.Models;

namespace Tests_server_app.Migrations
{
    [DbContext(typeof(TestsDbContext))]
    [Migration("20190505082242_fixedInitial")]
    partial class fixedInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Achievement", b =>
                {
                    b.Property<long>("AchievementId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<double>("Experience");

                    b.Property<long>("IconId");

                    b.Property<long>("ThemeId");

                    b.Property<string>("Title");

                    b.HasKey("AchievementId");

                    b.HasIndex("IconId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Answer", b =>
                {
                    b.Property<long>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerText");

                    b.HasKey("AnswerId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Icon", b =>
                {
                    b.Property<long>("IconId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Data");

                    b.HasKey("IconId");

                    b.ToTable("Icons");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Question", b =>
                {
                    b.Property<long>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QuestionText");

                    b.Property<int>("Weightiness");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.QuestionAnswer", b =>
                {
                    b.Property<long>("QuestionId");

                    b.Property<long>("AnswerId");

                    b.Property<bool>("IsRight");

                    b.HasKey("QuestionId", "AnswerId");

                    b.HasIndex("AnswerId");

                    b.ToTable("QuestionAnswer");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Permissions");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Test", b =>
                {
                    b.Property<long>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cheched");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<decimal>("LikesCount")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("Title");

                    b.HasKey("TestId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.TestQuestion", b =>
                {
                    b.Property<long>("TestId");

                    b.Property<long>("QuestionId");

                    b.Property<double>("WeightValue");

                    b.HasKey("TestId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("TestQuestion");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.TestTheme", b =>
                {
                    b.Property<long>("TestId");

                    b.Property<long>("ThemeId");

                    b.HasKey("TestId", "ThemeId");

                    b.HasIndex("ThemeId");

                    b.ToTable("TestTheme");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Theme", b =>
                {
                    b.Property<long>("ThemeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ThemeName");

                    b.HasKey("ThemeId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Login");

                    b.Property<string>("PasswordHash");

                    b.Property<long>("RoleId");

                    b.Property<string>("SecondName");

                    b.Property<int>("SignedUpWithAccount");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.UserAchievement", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("AchievementId");

                    b.Property<DateTime>("Date");

                    b.HasKey("UserId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("UserAchievement");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.UserTest", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("TestId");

                    b.Property<int>("CountAnsweredQuestions");

                    b.Property<int>("CountRightAnswers");

                    b.Property<DateTime>("DatePassed");

                    b.HasKey("UserId", "TestId");

                    b.HasIndex("TestId");

                    b.ToTable("UserTest");
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.Achievement", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Icon")
                        .WithMany("Achievements")
                        .HasForeignKey("IconId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tests_server_app.Models.DBModels.Theme")
                        .WithMany("Achievements")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.QuestionAnswer", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Answer", "Answer")
                        .WithMany("Questions")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tests_server_app.Models.DBModels.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.TestQuestion", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Question", "Question")
                        .WithMany("Tests")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tests_server_app.Models.DBModels.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.TestTheme", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Test", "Test")
                        .WithMany("Themes")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tests_server_app.Models.DBModels.Theme", "Theme")
                        .WithMany("Tests")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.User", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.UserAchievement", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Achievement", "Achievement")
                        .WithMany("Users")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tests_server_app.Models.DBModels.User", "User")
                        .WithMany("Achievements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tests_server_app.Models.DBModels.UserTest", b =>
                {
                    b.HasOne("Tests_server_app.Models.DBModels.Test", "Test")
                        .WithMany("Users")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tests_server_app.Models.DBModels.User", "User")
                        .WithMany("Tests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
