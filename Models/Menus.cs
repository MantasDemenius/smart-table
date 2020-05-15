using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class Menus
    {
        public Menus()
        {
            MenuDishes = new HashSet<MenuDishes>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
        public TimeSpan? TimeFrom { get; set; }
        public TimeSpan? TimeUntil { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateUntil { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MenuDishes> MenuDishes { get; set; }
    }
}
