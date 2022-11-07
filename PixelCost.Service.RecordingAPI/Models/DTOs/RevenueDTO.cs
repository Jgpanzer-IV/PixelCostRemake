namespace PixelCost.Service.RecordingAPI.Models.DTOs
{
    public class RevenueDTO
    {
        public long Id { get; set; }
        public string Task { get; set; } = string.Empty;
        public decimal EarningAmount { get; set; }
        public DateTime EarningDate { get; set; }

        public string UserId { get; set; } = string.Empty;
        public long DurationId { get; set; }
        public long? SubDurationId { get; set; }
        public long PaymentMethodId { get; set; }

    }
}
