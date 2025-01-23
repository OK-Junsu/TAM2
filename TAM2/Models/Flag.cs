using System.ComponentModel.DataAnnotations;

namespace TAM2.Models
{
    public abstract class Flag
    {
        [DataType(DataType.DateTime)]
        public DateTime DateAndTimeOfRegistration { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateAndTimeOfDeletion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
