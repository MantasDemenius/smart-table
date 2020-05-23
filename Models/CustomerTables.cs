using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smart_table.Models
{
    public partial class CustomerTables
    {
        public CustomerTables()
        {
            Bills = new HashSet<Bills>();
        }

        [Display(Name = "ID")]
        public long Id { get; set; }

        [Display(Name = "Vietų skaičius")]
        public int SeatsCount { get; set; }
        public string QrCode { get; set; }

        [Display(Name = "Užimtas")]
        public bool IsTaken { get; set; }
        public string JoinCode { get; set; }

        public virtual ICollection<Bills> Bills { get; set; }
    }
}
