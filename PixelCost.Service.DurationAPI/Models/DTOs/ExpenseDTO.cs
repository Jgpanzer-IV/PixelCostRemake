namespace PixelCost.Service.DurationAPI.Models.DTOs
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }

    }
}
