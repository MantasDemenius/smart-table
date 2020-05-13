using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class Discounts
    {
        public Discounts()
        {
            Bills = new HashSet<Bills>();
        }

        public long Id { get; set; }
        public string DiscountCode { get; set; }
        public DateTime? StandUntil { get; set; }
        public int DiscountProc { get; set; }

        public virtual ICollection<Bills> Bills { get; set; }
    }
}
