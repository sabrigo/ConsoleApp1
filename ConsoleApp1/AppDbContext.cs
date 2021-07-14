using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class AppDbContext : DbContext
    {
        public DbSet<LocationData> LocationData { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = rscamalertsample.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationData>()
                .Property(e => e.Latitude)
                .HasConversion<double>();
            modelBuilder.Entity<LocationData>()
                .Property(e => e.Longitude)
                .HasConversion<double>();
        }

    }

    public class LocationData
    {
        public LocationData(Guid id, double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Id = id;
        }

        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
