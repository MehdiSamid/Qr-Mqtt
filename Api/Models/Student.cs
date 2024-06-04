using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Student { 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public  string CIN { get; set; }
    public string? Email { get; set; }
    public int? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Filiere Filiere { get; set; }
    public ICollection<Attendance> Attendances { get; set; }  
}

}
