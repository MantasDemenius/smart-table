using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace smart_table.Models.DataBase
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}