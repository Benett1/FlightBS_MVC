using System;
using System.Reflection.Emit;
using FlightBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FlightBookingSystem
{
    public class DBContext : DbContext
    {
        public DBContext() : base()
        {
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<AirlineModel> Airlines { get; set; }
        public DbSet<AirportModel> Airports { get; set; }
        public DbSet<BookingsModel> Bookings { get; set; }
        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<PlaneModel> Planes { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlaneModel>()
                .HasKey(o => o.Id);

            builder.Entity<PlaneModel>()
                   .HasOne(e => e.AirlineModel)
                   .WithMany()
                   .HasForeignKey("AirlineId");


            builder.Entity<AirlineModel>()
                .HasKey(o => o.Id);

            builder.Entity<AirlineModel>()
                   .HasOne(e => e.LocationModel)
                   .WithMany()
                   .HasForeignKey("CountryOfOrigin");

            builder.Entity<AirportModel>()
               .HasKey(o => o.Id);

            builder.Entity<AirportModel>()
                   .HasOne(e => e.LocationModel)
                   .WithMany()
                   .HasForeignKey("LocationID");

            builder.Entity<BookingsModel>()
               .HasKey(o => o.Id);

            builder.Entity<BookingsModel>()
                   .HasOne(e => e.UserModel)
                   .WithMany()
                   .HasForeignKey("UserId");

            builder.Entity<BookingsModel>()
                   .HasOne(e => e.FlightModel)
                   .WithMany()
                   .HasForeignKey("FlightId");

            builder.Entity<FlightModel>()
              .HasKey(o => o.Id);

            builder.Entity<FlightModel>()
                   .HasOne(e => e.PlaneModel)
                   .WithMany()
                   .HasForeignKey("PlaneId");

            builder.Entity<FlightModel>()
                   .HasOne(e => e.DepartureAirportv)
                   .WithMany()
                   .HasForeignKey("DepartureAirport");

            builder.Entity<FlightModel>()
                   .HasOne(e => e.ArrivalAirportv)
                   .WithMany()
                   .HasForeignKey("ArrivalAirport");

            builder.Entity<RoleModel>()
                   .HasKey(o => o.Id);

            builder.Entity<RoleModel>()
                    .Property(r => r.AirlineId)
                    .IsRequired(false);

            builder.Entity<RoleModel>()
                   .HasOne(e => e.AirlineModel)
                   .WithMany()
                   .HasForeignKey("AirlineId")
                   .IsRequired(false);

            builder.Entity<UserModel>()
            .HasKey(o => o.Id);

            builder.Entity<UserModel>()
                   .HasOne(e => e.RoleModel)
                   .WithMany()
                   .HasForeignKey("RoleId");

        }
    }
}

