using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smart_table.Models
{
    public partial class OrderDishes
    {
        [Required(ErrorMessage = "Kiekis yra privalomas")]
        [Display(Name = "Kiekis")]
        public int Quantity { get; set; }

        [StringLength(255, ErrorMessage = "Komentaras yra per ilgas")]
        [Display(Name = "Komentaras")]
        public string Comment { get; set; }
        public long FkOrders { get; set; }
        public long FkDishes { get; set; }

        public virtual Dishes FkDishesNavigation { get; set; }
        public virtual Orders FkOrdersNavigation { get; set; }
    }
}
