namespace TAM2.Models
{
    /// <summary>
    /// Paid Time Off : 유급 휴가
    /// </summary>
    public class PaidTimeOff : Flag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
    }
}
