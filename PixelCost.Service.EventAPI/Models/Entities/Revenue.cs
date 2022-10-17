namespace PixelCost.Service.EventAPI.Models.Entities
{
    public class Revenue
    {
        public long Id { get; set; }
        public string Task { get; set; } = string.Empty;
        public decimal EarningAmount { get; set; }
        public DateTime EarningDate { get; set; }

        public long SubDurationId { get; set; } 
        public virtual SubDuration? SubDuration { get; set; }    
    }
}
