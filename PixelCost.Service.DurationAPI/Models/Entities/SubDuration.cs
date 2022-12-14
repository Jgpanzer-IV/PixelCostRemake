namespace PixelCost.Service.DurationAPI.Models.Entities
{
    public class SubDuration
    {
        public long Id { get; set; }    
        public string Name { get; set; } = string.Empty;
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int? TotalDays { get; set; }
        public int? RemainingDays { get; set; }
        public float? Progress { get; set; }
        public decimal InitialCost { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? SumCategoryCost { get; set; }
        public decimal? UsableMoney{ get; set; }
        public decimal? Revenue { get; set; }
        public int? RevenueCount { get; set; }
        public decimal? Expense { get;set; }
        public int? ExpenseCount { get;set; }
        public decimal? Balance { get; set; }
        public decimal? SumCategoryBalance { get; set; }
        public bool? IsAchived { get;set;}
        public long DurationId { get; set; }
        public virtual Duration? Duration { get; set; }

    }
}
