﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using smart_table.Models.DataBase;

namespace smart_table.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20200425115427_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("smart_table.Models.DataBase.RegisteredUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BirthDate")
                        .HasColumnName("birth_date")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnName("is_blocked")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnName("surname")
                        .HasColumnType("text");

                    b.Property<int?>("UserRoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("registered_user");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = "1990-10-10",
                            Email = "test@test.com",
                            IsBlocked = false,
                            Name = "William",
                            Password = "password",
                            Phone = "123",
                            RoleId = 0,
                            Surname = "Shakespeare"
                        });
                });

            modelBuilder.Entity("smart_table.Models.DataBase.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("smart_table.Models.DataBase.RegisteredUser", b =>
                {
                    b.HasOne("smart_table.Models.DataBase.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
