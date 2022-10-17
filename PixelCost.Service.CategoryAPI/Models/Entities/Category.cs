namespace PixelCost.Service.CategoryAPI.Models.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public decimal? Expense { get; set; }
        public int? ExpenseCount { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsAchived { get; set; }
        // Navigation reference
        public long DurationId { get; set; }
        public long SubDurationId { get; set; }
        public string UserId { get; set; } = string.Empty;

        public virtual IList<Expense>? Expenses { get; set; }

    }
}
