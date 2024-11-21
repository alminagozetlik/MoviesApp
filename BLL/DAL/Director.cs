using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public partial class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(60)]

        public string Surname { get; set; }

        public bool IsRetired { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}