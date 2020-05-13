using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smart_table.Models
{
    [Table("registered_user")]
    public partial class RegisteredUsers
    {
        public RegisteredUsers()
        {
            Orders = new HashSet<Orders>();
        }


        public long Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Vardo laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Vardas yra per ilgas")]
        [Display(Name = "Vardas")]
        public string Name { get; set; }

        [Column("surname")]
        [Required(ErrorMessage = "Pavardės laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Pavardė yra per ilga")]
        [Display(Name = "Pavardė")]
        public string Surname { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Slaptažodžio laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Slaptažodis yra per ilgas")]
        //[DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Slaptažodis turi būti bent 8 simbolių ir turėti bent 1 didžiąją raidę")]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Column("phone")]
        [Phone(ErrorMessage = "Telefono numeris neatitinka formato")]
        [Display(Name = "Telefono numeris")]
        public string Phone { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "El. pašto laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "El. paštas yra per ilgas")]
        [Display(Name = "El. paštas")]
        [EmailAddress(ErrorMessage = "El. pašto formatas yra netinkamas")]
        public string Email { get; set; }

        [Column("birth_date")]
        [Required(ErrorMessage = "Gimimo datos laukas yra privalomas")]
        [Display(Name = "Gimimo data")]
        [DataType(DataType.Date)] //Hide time picker
        public DateTime BirthDate { get; set; }

        [Column("is_blocked")]
        [Display(Name = "Būsena")]
        public bool IsBlocked { get; set; }

        [Column("role")]
        [Display(Name = "Rolė")]
        public long Role { get; set; }

        public virtual UserRole RoleNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
