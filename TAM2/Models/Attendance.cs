using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAM2.Models
{
    public class Attendance : Flag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }
        //public DayState State { get; set; }
        public string State { get; set; }
    }

    public readonly struct DayState
    {
        public DayStateType Value => _value;
        private readonly DayStateType _value;

        public DayState(DayStateType value) => _value = value;
        public DayState(string value) : this(ToEnum(value)) { }

        public override string ToString() => _value.ToString();

        public static DayStateType ToEnum(string s)
        {
            return s.Replace(" ", "") switch
            {
                "Absence" => DayStateType.Absence,
                "Arrive" => DayStateType.Arrive,
                "Leave" => DayStateType.Leave,
                "HalfDayOff" => DayStateType.HalfDayOff,
                "DayOff" => DayStateType.DayOff,
                _ => default
            };
        }

        public static DayState Parse(string s) => new(s);
    }

    public enum DayStateType
    {
        Absence,
        Arrive,
        Leave,
        HalfDayOff,
        DayOff
    }
}
