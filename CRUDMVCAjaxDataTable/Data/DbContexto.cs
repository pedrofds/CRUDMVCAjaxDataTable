using CRUDMVCAjaxDataTable.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMVCAjaxDataTable.Data
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Region> Region { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Region>().ToTable("Region");

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    CountryId = "US",
                    CountryNameEnglish = "United States of America"
                },
                new Country
                {
                    CountryId = "CA",
                    CountryNameEnglish = "Canada"
                });
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    CountryId = "US",
                    RegionId = "CT",
                    RegionNameEnglish = "Connecticut"
                },
                new Region
                {
                    CountryId = "US",
                    RegionId = "ME",
                    RegionNameEnglish = "Maine"
                },
                new Region
                {
                    CountryId = "US",
                    RegionId = "MA",
                    RegionNameEnglish = "Massachusetts"
                },
                new Region
                {
                    CountryId = "US",
                    RegionId = "NH",
                    RegionNameEnglish = "New Hampshire"
                },
                new Region
                {
                    CountryId = "US",
                    RegionId = "RI",
                    RegionNameEnglish = "Rhode Island"
                },
                new Region
                {
                    CountryId = "US",
                    RegionId = "VT",
                    RegionNameEnglish = "Vermont"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "NB",
                    RegionNameEnglish = "New Brunswick"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "NF",
                    RegionNameEnglish = "Newfoundland"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "NT",
                    RegionNameEnglish = "Northwest Territories"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "NS",
                    RegionNameEnglish = "Nova Scotia"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "NU",
                    RegionNameEnglish = "Nunavut"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "ON",
                    RegionNameEnglish = "Ontario"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "PE",
                    RegionNameEnglish = "Prince Edward Island"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "QC",
                    RegionNameEnglish = "Québec"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "SK",
                    RegionNameEnglish = "Saskatchewan"
                },
                new Region
                {
                    CountryId = "CA",
                    RegionId = "YT",
                    RegionNameEnglish = "Yukon"
                });
        }
    }
}
