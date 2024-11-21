using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [StringLength(8)]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
    }
}
