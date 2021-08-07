using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Infrastructure.Database.Entities
{
    [Table("Tble_User", Schema = "dbo")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int IdUser { get; set; }
        public int IdRol { get; set; }
        public string NameUser { get; set; }

 
 
    }
}
