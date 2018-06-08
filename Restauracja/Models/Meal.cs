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
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Składniki")]
        public string Ingredients { get; set; }

        public ICollection<Order_Meal> Order_Meal { get; set; }

    }
}