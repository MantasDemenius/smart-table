using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smart_table.Models
{
    public partial class Events
    {
        public long Id { get; set; }
        public long Type { get; set; }
        public long FkBills { get; set; }

        public virtual Bills FkBillsNavigation { get; set; }
        [Display(Name = "Pranešimas")]
        public virtual EventType TypeNavigation { get; set; }
    }
}
