using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public Pizza()
        {

        }

        public int PizzaId { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(30, ErrorMessage = "Il nome non può essere più lungo di 30 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(255, ErrorMessage = "Il nome non può essere più lungo di 255 caratteri")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(0, 100, ErrorMessage = "Scambi con Porsche Panamera?")]
        public double Price { get; set; }

    }
}
