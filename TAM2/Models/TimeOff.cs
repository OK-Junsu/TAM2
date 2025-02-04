using System.ComponentModel.DataAnnotations;

namespace TAM2.Models
{
    /// <summary>
    /// Time Off : 휴가
    /// </summary>
    public class TimeOff : Flag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        //public DayState Type { get; set; }
        public string Type { get; set; } = default!;
    }
}
