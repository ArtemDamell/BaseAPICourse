﻿// <auto-generated />
using System;
using DataStore.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataStore.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220110084356_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Models.EventAdministrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("EventAdministrators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Somestreet 1",
                            Age = 34,
                            FirstName = "Admin 1",
                            LastName = "Adminov 1",
                            Phone = "0409612987",
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "Somestreet 2",
                            Age = 23,
                            FirstName = "Admin 2",
                            LastName = "Adminov 2",
                            Phone = "0419397987",
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 3,
                            Address = "Somestreet 3",
                            Age = 40,
                            FirstName = "Admin 3",
                            LastName = "Adminov 3",
                            Phone = "0459697145",
                            ProjectId = 2
                        });
                });

            modelBuilder.Entity("Core.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Project 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Project 2"
                        });
                });

            modelBuilder.Entity("Core.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EnteredDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Owner")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Ticket for Project 1",
                            ProjectId = 1,
                            Title = "Ticket 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Ticket for Project 1",
                            ProjectId = 1,
                            Title = "Ticket 2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Ticket for Project 2",
                            ProjectId = 2,
                            Title = "Ticket 3"
                        });
                });

            modelBuilder.Entity("Core.Models.EventAdministrator", b =>
                {
                    b.HasOne("Core.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Core.Models.Ticket", b =>
                {
                    b.HasOne("Core.Models.Project", "Project")
                        .WithMany("Tickets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Core.Models.Project", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
