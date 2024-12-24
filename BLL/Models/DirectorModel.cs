using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DirectorModel // DTO
    {
        public Director Record {get; set;}
        [DisplayName("Director Name")]
        public string Name => Record.Name;
        [DisplayName("Director Surname")]
        public string Surname => Record.Surname;
        public string NameSurname => Record.Name + " " + Record.Surname;
        [DisplayName("Is Retired")]
        public string IsRetired => Record.IsRetired ? "Yes" : "No";
    }
}
