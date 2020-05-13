using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class OrderDishes
    {
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public long FkOrders { get; set; }
        public long FkDishes { get; set; }

        public virtual Dishes FkDishesNavigation { get; set; }
        public virtual Orders FkOrdersNavigation { get; set; }
    }
}
