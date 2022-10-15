 namespace PixelCost.Service.WalletAPI.Model.Entities
{
    public class PaymentMethod
    {
        public long Id { get;set; }
        public string PaymentName { get;set; } = string.Empty;
        public string PaymentType { get;set; } = string.Empty;
        public decimal? PaymentRevenue { get; set; }
        public int PaymentRevenueCount { get; set; }
        public decimal? PaymentBalance { get; set; }
        public decimal? PaymentExpense { get; set; }
        public int PaymentExpenseCount { get;set; } 
        public decimal? AverageUsedPerPayment { get;set; }
        public string Symbol { get; set; } = string.Empty;
        public string WalletID { get; set; } = string.Empty;
        public DateTime? DateCreate { get; set; }
        public virtual Wallet? Wallet { get; set; }  

    }
}
