namespace PixelCost.Service.EventAPI.Models.DTOs
{
    public class RevenueDTO
    {
        public long Id { get; set; }
        public string Task { get; set; } = string.Empty;
        public decimal EarningAmount { get; set; }
        public DateTime EarningDate { get; set; }

        public long SubDurationId { get; set; } 
     
    }
}
