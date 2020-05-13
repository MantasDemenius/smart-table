using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class DishCategories
    {
        public DishCategories()
        {
            Dishes = new HashSet<Dishes>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Dishes> Dishes { get; set; }
    }
}
