using System.ComponentModel.DataAnnotations;

namespace TAM2.Models
{
    public class User : Flag
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Join { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? Left { get; set; }
    }
}
