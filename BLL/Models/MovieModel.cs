using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class MovieModel
    {
        public Movie Record { get; set; }

        [DisplayName("Movie Name")]
        public string Name => Record.Name;
        [DisplayName("Release Date")]
        public string ReleaseDate => !Record.ReleaseDate.HasValue ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy");

        [DisplayName("Total Revenue")]
        public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("N2") : "0";
        [DisplayName("Director Name")]
        public string Director => Record.Director?.Name + " " + Record.Director?.Surname ;
     
        //public string NameSurname => Record.Director?.Name + " " + Record.Director?.Surname;


    }
}
