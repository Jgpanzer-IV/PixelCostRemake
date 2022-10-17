namespace PixelCost.Service.CategoryAPI.Models.Entities
{
    public class Expense
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }
        public long CategoryId { get; set; }
        public virtual Category? Category { get; set; }

    }
}
