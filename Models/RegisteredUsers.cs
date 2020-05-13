using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smart_table.Models
{
    public partial class RegisteredUsers
    {
        public RegisteredUsers()
        {
            Orders = new HashSet<Orders>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)] //Hide time picker
        public DateTime BirthDate { get; set; }
        public bool IsBlocked { get; set; }
        public long Role { get; set; }

        public virtual UserRole RoleNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
