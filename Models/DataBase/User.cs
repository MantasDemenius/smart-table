using System.ComponentModel.DataAnnotations.Schema;

namespace smart_table.Models.DataBase
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }


        [Column("last_name")]
        public string LastName { get; set; }


        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("date_of_birth")]
        public string DateOfBirth { get; set; }

        [Column("blocked")]
        public bool Blocked { get; set; }

        [Column("type")]
        public int Type { get; set; }
    }
}