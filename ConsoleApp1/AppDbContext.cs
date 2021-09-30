using Microsoft.EntityFrameworkCore;
using SQLite;
using System;

namespace ConsoleApp1
{
    public class AppDbContext : DbContext
    {
        public DbSet<LocationData> LocationData { get; set; }
        public DbSet<Setting> Setting { get; set; }

        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = E:\Workarea\ZZZFramework\SqliteIssue\ConsoleApp1\ConsoleApp1\rscamalertsample.db");
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

    public class Setting
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string DistanceInMeters { get; set; }
        public string ContinuousAlertDistance { get; set; }
        public bool IsContinuousAlert { get; set; }
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
