using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace la_mia_pizzeria_static.Context
{
    public class Restaurant : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> ListaPizze { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }

        public Restaurant()
        {

        }

        public Restaurant(DbContextOptions<Restaurant> options): base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-restaurant;Integrated Security=True");
        }
    }
}
