using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class Dishes
    {
        public Dishes()
        {
            DishIngredients = new HashSet<DishIngredients>();
            MenuDishes = new HashSet<MenuDishes>();
            OrderDishes = new HashSet<OrderDishes>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? Calories { get; set; }
        public double? Discount { get; set; }
        public long? FkDishCategories { get; set; }

        public virtual DishCategories FkDishCategoriesNavigation { get; set; }
        public virtual ICollection<DishIngredients> DishIngredients { get; set; }
        public virtual ICollection<MenuDishes> MenuDishes { get; set; }
        public virtual ICollection<OrderDishes> OrderDishes { get; set; }
    }
}
