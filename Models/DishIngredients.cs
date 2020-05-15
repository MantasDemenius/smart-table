using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class DishIngredients
    {
        public double Quantity { get; set; }
        public long FkDishes { get; set; }
        public long FkIngredients { get; set; }

        public virtual Dishes FkDishesNavigation { get; set; }
        public virtual Ingredients FkIngredientsNavigation { get; set; }
    }
}
