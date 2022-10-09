using la_mia_pizzeria_static.Context;
using la_mia_pizzeria_static.Models;
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

        public IActionResult Index()
        {

            using (Restaurant db = new Restaurant())
            {

                List<Pizza> pizzas = db.ListaPizze.Include("Category").OrderBy(pizza => pizza.Price).ToList();

                return View("Index", pizzas);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            CategoryPizza categoryPizza = new CategoryPizza();

            categoryPizza.Categories = new Restaurant().Categories.ToList();

            return View(categoryPizza);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryPizza data)
        {

            if (!ModelState.IsValid)
            {
                data.Categories = new Restaurant().Categories.ToList();
                return View(data);
            }


            using (Restaurant db = new Restaurant())
            {
                /*Pizza newPizza = new Pizza(); //istanzio una nuova pizza
                newPizza.Name = data.Pizza.Name;
                newPizza.Image = data.Pizza.Image;
                newPizza.Description = data.Pizza.Description;
                newPizza.Price = data.Pizza.Price;
                newPizza.CategoryId = data.Pizza.CategoryId;*/

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
                return NotFound();
            }

            CategoryPizza categoryPizza = new CategoryPizza();
            categoryPizza.Pizza = pizzaToEdit;
            categoryPizza.Categories = db.Categories.ToList();

            return View(categoryPizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryPizza data)
        {
            Restaurant db = new Restaurant();
            Pizza editedPizza = db.ListaPizze.Find(id);
            data.Categories = db.Categories.ToList();

            if (editedPizza != null)
            {
                editedPizza.Name = data.Pizza.Name;
                editedPizza.Description = data.Pizza.Description;
                editedPizza.Image = data.Pizza.Image;
                editedPizza.Price = data.Pizza.Price;
                editedPizza.CategoryId = data.Pizza.CategoryId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("La pizza che stai cercando non è presente");
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

            Pizza pizzaDetail = db.ListaPizze.Where(p => p.PizzaId == id).Include("Category").FirstOrDefault();

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