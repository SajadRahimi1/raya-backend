﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace raya_back.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Class", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("ClassCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Days")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstallmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("InstallmentPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrePaid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeHolding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotallHours")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("ClassCategories");
                });

            modelBuilder.Entity("ClassCategoryUser", b =>
                {
                    b.Property<Guid>("ReservedClassesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersReservedId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReservedClassesId", "UsersReservedId");

                    b.HasIndex("UsersReservedId");

                    b.ToTable("ClassCategoryUser");
                });

            modelBuilder.Entity("Nurse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Days")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Locations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("ReserveNurse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Days")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NurseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NurseId");

                    b.HasIndex("UserId");

                    b.ToTable("ReserveNurses");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BornCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergancyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Token")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClassCategory", b =>
                {
                    b.HasOne("Class", "Class")
                        .WithMany("ClassCategories")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("ClassCategoryUser", b =>
                {
                    b.HasOne("ClassCategory", null)
                        .WithMany()
                        .HasForeignKey("ReservedClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UsersReservedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReserveNurse", b =>
                {
                    b.HasOne("Nurse", "Nurse")
                        .WithMany("ReserveNurses")
                        .HasForeignKey("NurseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "UserReserved")
                        .WithMany("ReserveNurses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nurse");

                    b.Navigation("UserReserved");
                });

            modelBuilder.Entity("Class", b =>
                {
                    b.Navigation("ClassCategories");
                });

            modelBuilder.Entity("Nurse", b =>
                {
                    b.Navigation("ReserveNurses");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("ReserveNurses");
                });
#pragma warning restore 612, 618
        }
    }
}
