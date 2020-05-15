using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class Events
    {
        public long Id { get; set; }
        public long Type { get; set; }
        public long FkOrders { get; set; }

        public virtual Orders FkOrdersNavigation { get; set; }
        public virtual EventType TypeNavigation { get; set; }
    }
}
