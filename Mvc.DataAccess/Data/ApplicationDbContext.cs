using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;
using Mvc.Model;
using System.Security.Policy;
using System;

namespace Mvc.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id =1, Name="Action", DisplayOrder=1 },
                new Category { Id =2, Name="SciFi", DisplayOrder=2 },
                new Category { Id=3, Name ="History", DisplayOrder=3}
                ) ;

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id=1,
                    Title="Fortue Of Time",
                    Author="Billy Spark",
                    Description="Praesent vitae sodales libero.Praesent molestie orci augue, vitae euismod velit sollicitudin ac.Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="SWD9999001",
                    LastPrice=99,
                    Price=90,
                    Price50=85,
                    Price100=80

                },
                new Product
                {
                    Id=2,
                    Title="Dark Skies",
                    Author="Nancy Hoover",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="CAW777777701",
                    LastPrice=40,
                    Price=30,
                    Price50=25,
                    Price100=20
                },
                new Product
                {
                    Id=3,
                    Title="Vanish in the sunset",
                    Author="Julian Button",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="RITO5555501",
                    LastPrice=55,
                    Price=50,
                    Price50=40,
                    Price100=35
                },
                new Product
                {
                    Id=4,
                    Title="Cottone Candy",
                    Author= "Abby Muscles",
                    Description ="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="WS3333333301",
                    LastPrice=70,
                    Price=65,
                    Price50=60,
                    Price100=55
                },
                new Product
                {
                    Id=5,
                    Title="Rock in the ocean",
                    Author="Ron Parker",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="SOTJ1111111101",
                    LastPrice=30,
                    Price=27,
                    Price50=25,
                    Price100=20
                },
                new Product
                {
                    Id=6,
                    Title="Leaves and Wonders",
                    Author="laura  Phantom",
                    Description="Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="FOT000000001",
                    LastPrice=25,
                    Price=23,
                    Price50=22,
                    Price100=20
                }
            
            );
        }
    }
}
