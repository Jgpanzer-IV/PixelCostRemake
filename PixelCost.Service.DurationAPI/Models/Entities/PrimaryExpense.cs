namespace PixelCost.Service.DurationAPI.Models.Entities
{
    public class PrimaryExpense
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }

        public long DurationId { get; set; }
        public virtual Duration? Duration { get; set; }

    }
}
