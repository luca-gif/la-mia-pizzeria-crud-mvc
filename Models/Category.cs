namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public Category()
        {
        
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<Pizza>? Pizze { get; set; }

    }
}
