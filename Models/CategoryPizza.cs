namespace la_mia_pizzeria_static.Models
{
    public class CategoryPizza
    {
        public List<Category>? Categories { get; set; }
        public Pizza Pizza { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public List<int> SelectedIngredients { get; set; }

        public CategoryPizza()
        {
            Categories = new List<Category>();
            Pizza = new Pizza();
            Ingredients = new List<Ingredients>();
            SelectedIngredients = new List<int>();
        }
    }
}
