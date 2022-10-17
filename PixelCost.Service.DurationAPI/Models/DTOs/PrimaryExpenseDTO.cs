namespace PixelCost.Service.DurationAPI.Models.DTOs
{
    public class PrimaryExpenseDTO
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }
        public long DurationId { get; set; }

    }
}
