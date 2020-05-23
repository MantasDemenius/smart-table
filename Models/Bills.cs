using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Range(0,100000, ErrorMessage = "Arbatpinigiai būna daugiau nei 0")]
        [Display(Name = "Arbatpinigiai")]
        public double? Tips { get; set; }

        [Display(Name ="Užsakymo suma")]
        public double Amount { get; set; }
        public bool IsPaid { get; set; }

        [Display(Name ="Palikitę komentarą")]
        [StringLength(255, ErrorMessage = "Jūsų komentaras pražengė 255 raidžių limitą")]
        public string Evaluation { get; set; }

        [Display(Name = "Nuolaidos kodas")]
        public long? FkDiscounts { get; set; }
        public long FkCustomerTables { get; set; }

        public virtual Discounts FkDiscountsNavigation { get; set; }

        [Display(Name = "Staliukas")]
        public virtual CustomerTables FkCustomerTablesNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Events> Events { get; set; }
    }
}
