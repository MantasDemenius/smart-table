using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class Ingredients
    {
        public Ingredients()
        {
            DishIngredients = new HashSet<DishIngredients>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<DishIngredients> DishIngredients { get; set; }
    }
}
