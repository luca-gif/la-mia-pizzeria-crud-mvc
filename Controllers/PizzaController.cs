using la_mia_pizzeria_static.Context;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {

            using (Restaurant db = new Restaurant())
            {

                List<Pizza> pizzas = db.ListaPizze.Include("Category").OrderBy(pizza => pizza.Price).ToList();

                return View("Index", pizzas);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            CategoryPizza categoryPizza = new CategoryPizza();

            categoryPizza.Categories = new Restaurant().Categories.ToList();
            categoryPizza.Ingredients = new Restaurant().Ingredients.ToList();

            return View(categoryPizza);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryPizza data)
        {

            if (!ModelState.IsValid)
            {
                data.Categories = new Restaurant().Categories.ToList();
                data.Ingredients = new Restaurant().Ingredients.ToList();
                return View(data);
            }


            using (Restaurant db = new Restaurant())
            {

                data.Pizza.Ingredients = db.Ingredients.Where(ingredient => data.SelectedIngredients.Contains(ingredient.Id)).ToList();

                db.ListaPizze.Add(data.Pizza);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Restaurant db = new Restaurant();
            Pizza pizzaToEdit = db.ListaPizze.Where(p => p.PizzaId == id).FirstOrDefault();

            if (pizzaToEdit == null)
            {
                return NotFound("Pizza non trovata");
            }

            CategoryPizza categoryPizza = new CategoryPizza();
            categoryPizza.Pizza = pizzaToEdit;
            categoryPizza.Categories = db.Categories.ToList();
            categoryPizza.Ingredients = db.Ingredients.ToList();

            return View(categoryPizza);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryPizza data)
        {
            Restaurant db = new Restaurant();
            Pizza editedPizza = db.ListaPizze.Find(id);
            data.Categories = db.Categories.ToList();
            data.Ingredients = db.Ingredients.ToList();

            if (ModelState.IsValid)
            {
                editedPizza.Name = data.Pizza.Name;
                editedPizza.Description = data.Pizza.Description;
                editedPizza.Image = data.Pizza.Image;
                editedPizza.Price = data.Pizza.Price;
                editedPizza.CategoryId = data.Pizza.CategoryId;
                editedPizza.Ingredients = db.Ingredients.Where(ingredient => data.SelectedIngredients.Contains(ingredient.Id)).ToList();

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                data.Categories = new Restaurant().Categories.ToList();
                data.Ingredients = new Restaurant().Ingredients.ToList();

                return View("Edit", data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Restaurant db = new Restaurant();
            Pizza pizzaToDelete = db.ListaPizze.Where(p => p.PizzaId == id).FirstOrDefault();

            if (pizzaToDelete != null)
            {
                db.ListaPizze.Remove(pizzaToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("La pizza che stai cercando non è presente");
            }
        }

        public IActionResult Detail(int id)
        {
            Restaurant db = new Restaurant();

            Pizza pizzaDetail = db.ListaPizze.Where(p => p.PizzaId == id).Include("Category").Include("Ingredients").FirstOrDefault();

            if (pizzaDetail == null)
            {
                return NotFound("Ciò che stai cercando non è presente nel nostro database.");
            }
            else
            {
                return View(pizzaDetail);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}