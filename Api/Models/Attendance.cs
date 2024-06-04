using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceId { get; set; }
        [ForeignKey("Student")]
        public string CIN { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateOfAttendance { get; set; }
        public bool Status { get; set; } = false;
        public Student Student { get; set; }
    }
}