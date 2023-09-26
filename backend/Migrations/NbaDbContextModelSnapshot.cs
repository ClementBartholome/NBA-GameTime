﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Models;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(NbaDbContext))]
    partial class NbaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Game", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("gameID")
                        .HasColumnType("int");

                    b.Property<string>("home_team_full_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("home_team_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("home_team_score")
                        .HasColumnType("int");

                    b.Property<string>("visitor_team_full_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("visitor_team_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("visitor_team_score")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Standings", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("LastTenLosses")
                        .HasColumnType("int");

                    b.Property<int>("LastTenWins")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Streak")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Standings");
                });

            modelBuilder.Entity("Team", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("logo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("primaryColor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("teamName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
