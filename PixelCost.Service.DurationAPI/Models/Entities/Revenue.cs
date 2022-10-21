namespace PixelCost.Service.DurationAPI.Models.Entities
{
    public class Revenue
    {
        public long Id { get;set; } 
        public string Task { get;set; } = string.Empty;
        public decimal EarningAmount { get;set; }
        public DateTime EarningDate { get;set; }    

        public long DurationId { get;set; }
        public virtual Duration? Duration { get;set; }   

    }
}
