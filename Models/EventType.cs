using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class EventType
    {
        public EventType()
        {
            Events = new HashSet<Events>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
