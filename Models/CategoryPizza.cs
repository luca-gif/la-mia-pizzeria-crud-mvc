namespace la_mia_pizzeria_static.Models
{
    public class CategoryPizza
    {
        public List<Category>? Categories { get; set; }
        public Pizza Pizza { get; set; }

        public CategoryPizza()
        {
            Categories = new List<Category>();
            Pizza = new Pizza();
        }
    }
}
