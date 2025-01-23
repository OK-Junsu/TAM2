using System.ComponentModel.DataAnnotations;

namespace TAM2.Models
{
    /// <summary>
    /// Non-Business Day : 휴업일
    /// </summary>
    public class NonBusinessDay : Flag
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        public NonBusinessDayType Type { get; set; }
        [DataType(DataType.Duration)]
        public TimeSpan ReducedTime { get; set; }
    }

    public enum NonBusinessDayType
    {
        WorkingDay,
        Weenend,
        Holiday,
        Reduced
    }
}
