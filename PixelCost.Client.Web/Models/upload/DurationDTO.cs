

namespace PixelCost.Client.Web.Models.upload
{
    public class DurationDTO
    {
        public string Name {get;set;} = string.Empty;
        public DateTime StartingDate {get;set;}
        public DateTime EndingDate {get;set;} 
        public int RemainDate {get;set;}  
        public byte Progress {get;set;}
        public decimal Cost {get;set;}
        public decimal CostSubDuration {get;set;}
        public decimal Expense {get;set;}
        public decimal AverageExpense {get;set;}
        public decimal Balance {get;set;}
        public decimal Untouched {get;set;}
        public bool IsActive {get;set;}

        public List<SubDurationDTO>? SubDurations {get;set;}
        public List<PrimaryExpenseDTO>? PrimaryExpenses {get;set;}
        public List<RevenueDTO>? Revenues {get;set;}
    }
}