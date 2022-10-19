namespace PixelCost.Service.PaymentAPI.Models.Entities
{
    public class Revenue
    {

        public long Id { get; set; }
        public string Task { get; set; } = string.Empty;
        public decimal EarningAmount { get; set; }
        public DateTime EarningDate { get; set; }
        public long PaymentId { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
    
    }
}
