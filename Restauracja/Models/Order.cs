using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restauracja.Models
{
    public class Order
    {
        public Order()
        {
            this.Order_Meal = new HashSet<Order_Meal>();
        }

        [Display(Name = "Id:")]
        public int Id { get; set; } 

        [Display(Name = "Id kelnera:")]
        public string WaiterId { get; set; }

        [Display(Name ="Numer stolika")]
        [Range(1,10)]
        public int Table { get; set; }

        [Display(Name = "Czas złożenia zamówienia")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime OrderTime { get; set; }

        [Display(Name = "Czas realizacji zamówienia")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime? MealTime { get; set; }

        public virtual ICollection<Order_Meal> Order_Meal { get; set; }
        public virtual User Waiter { get; set; }

    }
}