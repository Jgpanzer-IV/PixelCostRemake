namespace PixelCost.Service.RecordingAPI.Models.DTOs
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public string OrderingName { get; set; } = string.Empty;
        public decimal OrderingPrice { get; set; }
        public DateTime OrderingDate { get; set; }

        public string UserId { get; set; } = string.Empty;
        public long DurationId { get; set; }
        public long SubDurationId { get; set; }
        public long CategoryId { get; set; }
    }
}
