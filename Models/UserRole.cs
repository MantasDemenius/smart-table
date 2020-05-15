using System;
using System.Collections.Generic;

namespace smart_table.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            RegisteredUsers = new HashSet<RegisteredUsers>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RegisteredUsers> RegisteredUsers { get; set; }
    }
}
