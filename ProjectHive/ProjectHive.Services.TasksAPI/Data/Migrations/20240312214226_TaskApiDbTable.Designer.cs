﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectHive.Services.TasksAPI.Data;


#nullable disable

namespace ProjectHive.Services.TasksAPI.Migrations
{
    [DbContext(typeof(ProjectHiveTasksDbContext))]
    [Migration("20240312214226_TaskApiDbTable")]
    partial class TaskApiDbTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectHive.Services.TasksAPI.Data.Entities.StatusTasks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StatusGoals");
                });

            modelBuilder.Entity("ProjectHive.Services.TasksAPI.Data.Entities.Tasks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartExicution")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StatusTaskIdId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StatusTaskIdId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProjectHive.Services.TasksAPI.Data.Entities.Tasks", b =>
                {
                    b.HasOne("ProjectHive.Services.TasksAPI.Data.Entities.StatusTasks", "StatusTaskId")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusTaskIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusTaskId");
                });

            modelBuilder.Entity("ProjectHive.Services.TasksAPI.Data.Entities.StatusTasks", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
