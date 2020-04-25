using System.ComponentModel.DataAnnotations.Schema;

namespace smart_table.Models.DataBase
{
    [Table("registered_user")]
    public class RegisteredUser : BaseEntity
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("birth_date")]
        public string BirthDate { get; set; }

        [Column("role")]
        public UserRoleEnum Role { get; set; }

        [Column("is_blocked")]
        public bool IsBlocked { get; set; }
    }

}