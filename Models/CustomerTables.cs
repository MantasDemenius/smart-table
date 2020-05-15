using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class CustomerTables
    {
        public CustomerTables()
        {
            Orders = new HashSet<Orders>();
        }

        public long Id { get; set; }
        public int SeatsCount { get; set; }
        public string QrCode { get; set; }
        public bool IsTaken { get; set; }
        public string JoinCode { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
