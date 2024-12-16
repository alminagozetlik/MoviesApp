using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public partial class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        public string RoleName { get; set; }

        [InverseProperty("Role")]
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
