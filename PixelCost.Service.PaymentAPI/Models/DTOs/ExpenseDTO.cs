namespace PixelCost.Service.PaymentAPI.Models.DTOs
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }
        public long PaymentId { get; set; }

    }
}
