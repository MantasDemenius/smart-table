using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class Bills
    {
        public Bills()
        {
            Events = new HashSet<Events>();
            Orders = new HashSet<Orders>();
        }

        public long Id { get; set; }
        public DateTime? DateTime { get; set; }
        public double? Tips { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }
        public string Evaluation { get; set; }
        public long? FkDiscounts { get; set; }

        public virtual Discounts FkDiscountsNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Events> Events { get; set; }
    }
}
