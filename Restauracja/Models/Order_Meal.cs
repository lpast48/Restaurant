﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restauracja.Models
{
    public class Order_Meal
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MealId { get; set; }

        [Display(Name = "Czas wydania")]
        public System.DateTime? IssueTime { get; set; }

        public virtual Order Order { get; set; }
        public virtual Meal Meal { get; set; }
    }
}