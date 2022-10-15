namespace PixelCost.Service.PaymentAPI.Models.Entities
{
    public class Expense
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }
        public long PaymentId { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }

    }
}
