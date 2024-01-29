using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {
            
        }
        public DbSet<Villa> Villas {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "First Villa 1",
                    Details = "Some big Villa, with detailed info about it",
                    ImageUrl = "https://cdn2.thecatapi.com/images/eda.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Second Villa 2",
                    Details = "Smoller Villa, with less info about it",
                    ImageUrl = "https://cdn2.thecatapi.com/images/752.jpg",
                    Occupancy = 3,
                    Rate = 100,
                    Sqft = 300,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Third Villa 3",
                    Details = "Medium Villa, with info about it",
                    ImageUrl = "https://cdn2.thecatapi.com/images/5n9.jpg",
                    Occupancy = 4,
                    Rate = 400,
                    Sqft = 450,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                 new Villa
                 {
                     Id = 4,
                     Name = "Forth Villa 4",
                     Details = "Large Villa",
                     ImageUrl = "https://cdn2.thecatapi.com/images/4qp.jpg",
                     Occupancy = 6,
                     Rate = 600,
                     Sqft = 700,
                     Amenity = "",
                     CreatedDate = DateTime.Now,
                 }
                );
            //base.OnModelCreating(modelBuilder);
        }

    }
}
