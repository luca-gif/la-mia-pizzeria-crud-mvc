using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;


namespace la_mia_pizzeria_static.Context
{
    public class Restaurant : DbContext
    {
        public DbSet<Pizza> ListaPizze { get; set; }
        public DbSet<Category> Categories { get; set; }

        public Restaurant() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-restaurant;Integrated Security=True");
        }
    }
}
