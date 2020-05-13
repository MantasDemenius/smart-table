using System;
using System.Collections.Generic;

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
        public DateTime? DateTime { get; set; }
        public double? Temperature { get; set; }
        public bool Submitted { get; set; }
        public bool Served { get; set; }
        public long? FkBills { get; set; }
        public long? FkRegisteredUsers { get; set; }
        public long FkCustomerTables { get; set; }

        public virtual Bills FkBillsNavigation { get; set; }
        public virtual CustomerTables FkCustomerTablesNavigation { get; set; }
        public virtual RegisteredUsers FkRegisteredUsersNavigation { get; set; }
        public virtual ICollection<Events> Events { get; set; }
        public virtual ICollection<OrderDishes> OrderDishes { get; set; }
    }
}
