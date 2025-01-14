using System.ComponentModel.DataAnnotations;

namespace TAM2.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string? Name { get; set; }
        [DataType(DataType.DateTime), ]
        public DateTime Arrive { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Leave { get; set; }
    }
}
