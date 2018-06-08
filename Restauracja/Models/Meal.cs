using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restauracja.Models
{
    public class Meal
    {
        public Meal()
        {
            this.Order_Meal = new HashSet<Order_Meal>();
        }

        [Display(Name = "nr posiłku")]
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        [MinLength(20)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Składniki")]
        [MinLength(10)]
        [Required]
        public string Ingredients { get; set; }

        [Display(Name = "Cena")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "Alergeny")]
        [Required]
        public string Allergens { get; set; }

        public ICollection<Order_Meal> Order_Meal { get; set; }

    }
}