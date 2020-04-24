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
    }
}