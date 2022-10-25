using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Models.content
{
    public class FinancialSummarizing
    {
        public string Title {get;init;}
        public string MainLabel01 {get; init;}
        public string MainLabel02 {get; init;}
        public string MainValue01 {get; init;}
        public string MainValue02 {get; init;}
        public string color {get;init;}
        public string SumLabel {get; init;}
        public string SumValue {get; init;}
        public List<string>? ListLabel {get;}
        public List<string>? ListValue {get;}

        /// <summary>
        /// Initiate a new instance with value summarized by duration object  
        /// </summary>
        /// <param name="durationDTO"></param>
        /// <param name="status"></param>
        public FinancialSummarizing(DurationDTO durationDTO,char status = 'e')
        {
            switch(status){
                case 'e':
                    Title = "Expense";
                    MainLabel01 = "Total Expense";
                    MainValue01 = durationDTO.Expense?.ToString("C") ?? "0";
                    MainLabel02 = "Average per day";
                    MainValue02 = (durationDTO.Expense / durationDTO.ExpenseCount)?.ToString("C") ?? "0";
                    SumLabel = "Total Sub duration expense";
                    SumValue = durationDTO.SubDurations?.Sum(e => e.Expense).ToString() ?? string.Empty;
                    color = "e30000";
                    ListLabel = new List<string>();
                    ListValue = new List<string>();
                    for(byte i=0; i< (durationDTO.SubDurations?.Count() ?? 0); i++){
                        ListLabel.Add(durationDTO.SubDurations?.ElementAt(i).Name ?? "");
                        ListValue.Add(durationDTO.SubDurations?.ElementAt(i).Expense.ToString() ?? "");
                    }
                break;

                case 'b':
                    Title = "Balance";
                    MainLabel01 = "Total Balance";
                    MainValue01 = durationDTO.Balance?.ToString("C") ?? "0";
                    MainLabel02 = "Untouched";
                    MainValue02 = durationDTO.UsableMoney?.ToString("C") ?? "0";
                    SumLabel = "Total Sub duration balance";
                    SumValue = durationDTO.SubDurations?.Sum(e => e.Balance).ToString() ?? string.Empty;
                    color = "88cc00";
                    ListLabel = new List<string>();
                    ListValue = new List<string>();
                    for(byte i=0; i< (durationDTO.SubDurations?.Count() ?? 0); i++){
                        ListLabel.Add(durationDTO.SubDurations?.ElementAt(i).Name ?? "");
                        ListValue.Add(durationDTO.SubDurations?.ElementAt(i).Balance.ToString() ?? "");
                    }
                break;

                default:
                    Title = string.Empty;
                    MainValue01 = string.Empty;
                    MainValue02 = string.Empty;
                    MainLabel01 = string.Empty;
                    MainLabel02 = string.Empty;
                    SumLabel = string.Empty;
                    SumValue = string.Empty;
                    ListLabel = null;
                    ListValue = null;
                    color = "333333";
                break;
            }

        }
    }
}