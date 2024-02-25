﻿// <auto-generated />
using System;
using AppointmentsManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointmentsManager.DataAccess.Migrations
{
    [DbContext(typeof(AppointmentsDbContext))]
    partial class AppointmentsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Address", b =>
                {
                    b.Property<int>("addressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT(10)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("address2")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("cityId")
                        .HasColumnType("INT(10)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("createdBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("lastUpdateBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("postalCode")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("addressId");

                    b.HasIndex("cityId");

                    b.ToTable("address");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Appointment", b =>
                {
                    b.Property<int>("appointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT(10)");

                    b.Property<string>("contact")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("createdBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<int>("customerId")
                        .HasColumnType("INT(10)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("end")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("lastUpdateBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("start")
                        .HasColumnType("DATETIME");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<int>("userId")
                        .HasColumnType("INT");

                    b.HasKey("appointmentId");

                    b.HasIndex("customerId");

                    b.HasIndex("userId");

                    b.ToTable("appointment");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.City", b =>
                {
                    b.Property<int>("cityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT(10)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("countryId")
                        .HasColumnType("INT(10)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("createdBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("lastUpdateBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("cityId");

                    b.HasIndex("countryId");

                    b.ToTable("city");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Country", b =>
                {
                    b.Property<int>("countryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT(10)");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("createdBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("lastUpdateBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("countryId");

                    b.ToTable("country");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Customer", b =>
                {
                    b.Property<int>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT(10)");

                    b.Property<sbyte>("active")
                        .HasColumnType("TINYINT");

                    b.Property<int>("addressId")
                        .HasColumnType("INT(10)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("createdBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(45)");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("lastUpdateBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.HasKey("customerId");

                    b.HasIndex("addressId");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    b.Property<sbyte>("active")
                        .HasColumnType("TINYINT");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("createdBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("lastUpdateBy")
                        .IsRequired()
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("userId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Address", b =>
                {
                    b.HasOne("AppointmentsManager.DataAccess.Models.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Appointment", b =>
                {
                    b.HasOne("AppointmentsManager.DataAccess.Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppointmentsManager.DataAccess.Models.User", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.City", b =>
                {
                    b.HasOne("AppointmentsManager.DataAccess.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("countryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Customer", b =>
                {
                    b.HasOne("AppointmentsManager.DataAccess.Models.Address", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("addressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Address", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.City", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.Customer", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("AppointmentsManager.DataAccess.Models.User", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
