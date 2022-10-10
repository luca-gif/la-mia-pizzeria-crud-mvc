using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredients
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<Pizza>? Pizze { get; set; }

    }
}
