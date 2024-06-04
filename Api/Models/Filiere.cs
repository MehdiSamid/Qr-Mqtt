using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Filiere
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int FiliereId { get; set; }
        public string? FiliereName { get; set; }
        //public ICollection<Student> Students { get; set; } = [];
    }
}
