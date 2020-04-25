using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace smart_table.Models.DataBase
{
    [Table("registered_user")]
    public class RegisteredUser : BaseEntity
    {
        public enum UserRoleEnum
        {
            Administrator,
            Waiter
        }

        [Column("name")]
        [Required(ErrorMessage = "Vardo laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Vardas yra per ilgas")]
        [Display(Name="Vardas")]
        public string Name { get; set; }

        [Column("surname")]
        [Required(ErrorMessage = "Pavardės laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Pavardė yra per ilga")]
        [Display(Name = "Pavardė")]
        public string Surname { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "El. pašto laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "El. paštas yra per ilgas")]
        [Display(Name = "El. paštas")]
        [EmailAddress(ErrorMessage = "El. pašto formatas yra netinkamas")]
        public string Email { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Slaptažodžio laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Slaptažodis yra per ilgas")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Slaptažodis turi būti bent 8 simbolių ir turėti bent 1 didžiąją raidę")]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Column("phone")]
        [Phone(ErrorMessage = "Telefono numeris neatitinka formato")]
        [Display(Name = "Telefono numeris")]
        public string Phone { get; set; }


        [Column("birth_date")]
        [Required(ErrorMessage = "Gimimo datos laukas yra privalomas")]
        [StringLength(255, ErrorMessage = "Gimimo data yra per ilga")]
        [Display(Name = "Gimimo data")]
        public string BirthDate { get; set; }

        [Column("role")]
        [Display(Name = "Rolė")]
        public UserRoleEnum UserRole { get; set; } = UserRoleEnum.Waiter;

        [Column("is_blocked")]
        [Display(Name = "Užblokuoti")]
        public bool IsBlocked { get; set; } = false;
    }
}