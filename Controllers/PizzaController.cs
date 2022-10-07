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
                Pizza newPizza = new Pizza(); //istanzio una nuova pizza
                newPizza.Name = data.Pizza.Name;
                newPizza.Image = data.Pizza.Image;
                newPizza.Description = data.Pizza.Description;
                newPizza.Price = data.Pizza.Price;

                db.ListaPizze.Add(newPizza);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Restaurant db = new Restaurant();
            Pizza pizzaToEdit = db.ListaPizze.Where(p => p.PizzaId == id).FirstOrDefault();

            return View(pizzaToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pizza data)
        {
            Restaurant db = new Restaurant();
            Pizza editedPizza = db.ListaPizze.Find(id);

            if (editedPizza != null)
            {
                editedPizza.Name = data.Name;
                editedPizza.Description = data.Description;
                editedPizza.Image = data.Image;
                editedPizza.Price = data.Price;

                db.SaveChanges();
                return View("Detail", data);
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

            Pizza PizzaDetail = db.ListaPizze.Where(p => p.PizzaId == id).FirstOrDefault();

            if (PizzaDetail == null)
            {
                return NotFound("Ciò che stai cercando non è presente nel nostro database.");
            }
            else
            {
                return View(PizzaDetail);
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