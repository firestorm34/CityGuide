using CityGuide.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityGuide.Shared.DataAccess
{
    
    public  class CityGuideContext: DbContext
    {
        public DbSet<City> Cities { get; set; } 
        public CityGuideContext(DbContextOptions<CityGuideContext> options):base(options)
        {
            LoadCities();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public async void LoadCities() {

            if (!Cities.Any())
            {
                List<City> citiesList = new() {
                    new City
                    {
                        FoundationDate = new DateTime(2002, 12, 11),
                        Name = "New York",
                        Population = 13000
                    },
                    new City
                    {
                        FoundationDate = new DateTime(2001, 12, 11),
                        Name = "Moscow",
                        Population = 11000
                    },
                    new City
                    {
                        FoundationDate = new DateTime(2001, 12, 11),
                        Name = "London",
                        Population = 11000
                    },
                    new City
                    {
                    FoundationDate = new DateTime(2001, 12, 11),
                    Name = "Chicago",
                    Population = 11000
                    },

                };

                Cities.AddRange(citiesList);
                await SaveChangesAsync();

            }
        }
    }
}
