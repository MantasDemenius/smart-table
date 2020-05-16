using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smart_table.Models
{
    public partial class Orders
    {
        public Orders()
        {
            Events = new HashSet<Events>();
            OrderDishes = new HashSet<OrderDishes>();
        }

        public long Id { get; set; }
       
        [Display(Name = "Data ir laikas")]
        public DateTime? DateTime { get; set; }

        [Display(Name = "Temperatūra")]
        public double? Temperature { get; set; }

        [Display(Name = "Pateiktas")]
        public bool Submitted { get; set; }

        [Display(Name = "Aptarnautas")]
        public bool Served { get; set; }

        public long? FkBills { get; set; }

        public long? FkRegisteredUsers { get; set; }

        public long FkCustomerTables { get; set; }

        [Display(Name = "Sąskaitos ID")]
        public virtual Bills FkBillsNavigation { get; set; }
        
        [Display(Name = "Staliukas")]
        public virtual CustomerTables FkCustomerTablesNavigation { get; set; }
        
        [Display(Name = "Padavėjas")]
        public virtual RegisteredUsers FkRegisteredUsersNavigation { get; set; }
        public virtual ICollection<Events> Events { get; set; }
        public virtual ICollection<OrderDishes> OrderDishes { get; set; }
    }
}
