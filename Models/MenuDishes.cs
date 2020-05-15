using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class MenuDishes
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateUntil { get; set; }
        public long FkDishes { get; set; }
        public long FkMenus { get; set; }

        public virtual Dishes FkDishesNavigation { get; set; }
        public virtual Menus FkMenusNavigation { get; set; }
    }
}
