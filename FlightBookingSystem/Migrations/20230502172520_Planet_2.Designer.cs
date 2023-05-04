﻿// <auto-generated />
using System;
using FlightBookingSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightBookingSystem.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230502172520_Planet_2")]
    partial class Planet_2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FlightBookingSystem.Models.AirlineModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CountryOfOrigin")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CountryOfOrigin");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.AirportModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LocationID")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("Id");

                    b.HasIndex("LocationID");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.BookingsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<int>("Seat")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("baggage")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.FlightModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ArrivalAirport")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DepartureAirport")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PlaneId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalAirport");

                    b.HasIndex("DepartureAirport");

                    b.HasIndex("PlaneId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.LocationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.PlaneModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AirlineId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Model")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<double>("Payload")
                        .HasColumnType("double");

                    b.Property<int>("Seat")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid?>("AirlineId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Password")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.AirlineModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.LocationModel", "LocationModel")
                        .WithMany()
                        .HasForeignKey("CountryOfOrigin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationModel");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.AirportModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.LocationModel", "LocationModel")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationModel");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.BookingsModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.FlightModel", "FlightModel")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.Models.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlightModel");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.FlightModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.AirportModel", "ArrivalAirportv")
                        .WithMany()
                        .HasForeignKey("ArrivalAirport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.Models.AirportModel", "DepartureAirportv")
                        .WithMany()
                        .HasForeignKey("DepartureAirport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.Models.PlaneModel", "PlaneModel")
                        .WithMany()
                        .HasForeignKey("PlaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalAirportv");

                    b.Navigation("DepartureAirportv");

                    b.Navigation("PlaneModel");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.PlaneModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.AirlineModel", "AirlineModel")
                        .WithMany()
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirlineModel");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.RoleModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.AirlineModel", "AirlineModel")
                        .WithMany()
                        .HasForeignKey("AirlineId");

                    b.Navigation("AirlineModel");
                });

            modelBuilder.Entity("FlightBookingSystem.Models.UserModel", b =>
                {
                    b.HasOne("FlightBookingSystem.Models.RoleModel", "RoleModel")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleModel");
                });
#pragma warning restore 612, 618
        }
    }
}
